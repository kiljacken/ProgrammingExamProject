using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Eksamensprojekt
{
    public class Quiz
    {
        public List<QuestionReference> QuestionReferences = new List<QuestionReference>();
        public List<ImageReference> ImageReferences = new List<ImageReference>();

        // XmlIgnore make the XMLSerializer not store this field
        [XmlIgnore]
        public List<Question> Questions = new List<Question>();

        [XmlIgnore]
        public List<Image> Images = new List<Image>();

        /// <summary>
        /// Adds a new question to the quiz
        /// </summary>
        /// <returns>The newly created and added question</returns>
        public Question AddQuestion()
        {
            Question question = new Question();
            question.QuestionId = String.Format(StringResources.MISC_QUESTION_ID, Questions.Count);

            Questions.Add(question);

            return question;
        }

        /// <summary>
        /// Removes a question from the quiz
        /// </summary>
        /// <param name="question">The question to remove</param>
        public void RemoveQuestion(Question question)
        {
            Questions.Remove(question);
        }

        /// <summary>
        /// Removes an image from the quiz
        /// </summary>
        /// <param name="index">Index of the image to remove</param>
        public void RemoveImage(int index)
        {
            // Loop through all images
            // If image index > removed index, decrement the index to keep it pointing to the right image
            // If image index == removed index, set index to -1 (no image selected)
            foreach (var question in Questions)
            {
                if (question.ImageIndex > index)
                {
                    question.ImageIndex--;
                }
                else if (question.ImageIndex == index)
                {
                    question.ImageIndex = -1;
                }
            }

            Images.RemoveAt(index);
        }

        /// <summary>
        /// Loads a quiz from a zip file
        /// </summary>
        /// <param name="path">The path of the zip file</param>
        /// <returns>The loaded quiz, or null if an error occured</returns>
        public static Quiz LoadFromZip(String path)
        {
            Quiz quiz = null;

            try
            {
                // Open the zip file
                using (ZipArchive archive = ZipFile.Open(path, ZipArchiveMode.Read))
                {
                    // Get the serialized Quiz object
                    ZipArchiveEntry quizEntry = archive.GetEntry(StringResources.PATH_QUIZ);

                    // Dezeriale the quiz
                    using (Stream stream = quizEntry.Open())
                    {
                        XmlSerializer x = new XmlSerializer(typeof(Quiz));
                        quiz = (Quiz)x.Deserialize(stream);
                    }

                    // Load all referenced questions
                    quiz.Questions = new List<Question>(quiz.QuestionReferences.Count);
                    for (int i = 0; i < quiz.QuestionReferences.Count; i++)
                    {
                        QuestionReference reference = quiz.QuestionReferences[i];
                        quiz.Questions.Add(reference.LoadQuestion(archive));
                    }

                    // Load all references images
                    quiz.Images = new List<Image>(quiz.ImageReferences.Count);
                    for (int i = 0; i < quiz.ImageReferences.Count; i++)
                    {
                        ImageReference reference = quiz.ImageReferences[i];
                        quiz.Images.Add(reference.LoadImage(archive));
                    }
                }
            }
            catch(Exception e)
            {
                // Show a message box on error
                MessageBox.Show(String.Format(StringResources.ERROR_LOAD_QUIZ, e.ToString()));
                return null;
            }

            return quiz;
        }

        /// <summary>
        /// Stores a quiz to a zip file
        /// </summary>
        /// <param name="quiz">The quiz to store</param>
        /// <param name="path">The path of the zip file</param>
        public static void StoreToZip(Quiz quiz, String path)
        {
            // Create a backup of the quiz if it already exists, in case something goes wrong
            Boolean cleanup = false;
            string backup = path + StringResources.MISC_BACKUP_EXTENSION;
            if (File.Exists(path))
            {
                // Delete existing backup
                // Shouldn't exist, but just in case it might
                if (File.Exists(backup))
                    File.Delete(backup);

                // Rename file
                File.Move(path, backup);
                cleanup = true;
            }

            try
            {
                // Open the zip file
                using (ZipArchive archive = ZipFile.Open(path, ZipArchiveMode.Create))
                {
                    // Rebuild image references and save images
                    quiz.ImageReferences.Clear();
                    for (int i = 0; i < quiz.Images.Count; i++)
                    {
                        Image image = quiz.Images[i];
                        ImageReference reference = new ImageReference { Path = String.Format(StringResources.PATH_IMAGE, i)};
                        quiz.ImageReferences.Add(reference);

                        reference.SaveImage(image, archive);
                    }

                    // Rebuild question references and save questions
                    quiz.QuestionReferences.Clear();
                    for (int i = 0; i < quiz.Questions.Count; i++)
                    {
                        Question question = quiz.Questions[i];
                        QuestionReference reference = new QuestionReference { Path = String.Format(StringResources.PATH_QUESTION, i) };
                        quiz.QuestionReferences.Add(reference);

                        reference.SaveQuestion(question, archive);
                    }

                    // Create file in archive
                    ZipArchiveEntry quizEntry = archive.CreateEntry(StringResources.PATH_QUIZ);
                    using (Stream stream = quizEntry.Open())
                    {
                        // Serialize the Quiz object to the file
                        XmlSerializer x = new XmlSerializer(typeof(Quiz));
                        x.Serialize(stream, quiz);
                    }
                }
            }
            catch(Exception e)
            {
                // On error, restore backup
                if (cleanup && File.Exists(backup))
                {
                    File.Move(backup, path);
                    File.Delete(backup);

                    return;
                }

                // Then show an error message
                MessageBox.Show(String.Format(StringResources.ERROR_SAVE_QUIZ, e.ToString()));
            }

            // Delete backup if all goes well
            if (cleanup && File.Exists(backup))
            {
                File.Delete(backup);
                return;
            }
        }
    }

    [Serializable()]
    public class QuestionReference
    {
        public String Path = "";

        /// <summary>
        /// Loads a question from a zip archibe
        /// </summary>
        /// <param name="archive">The archive to load from</param>
        /// <returns>The loaded question</returns>
        public Question LoadQuestion(ZipArchive archive)
        {
            Question question = null;
            // Open file in zip
            using (Stream stream = archive.GetEntry(StringResources.PATH_QUESTION_DIR + Path).Open())
            {
                // Deserialize the question
                XmlSerializer x = new XmlSerializer(typeof(Question));
                question = (Question)x.Deserialize(stream);
            }

            return question;
        }

        /// <summary>
        /// Saves a question to a zip archive
        /// </summary>
        /// <param name="question">The question to save</param>
        /// <param name="archive">The archive to save to</param>
        public void SaveQuestion(Question question, ZipArchive archive)
        {
            // Create file in zip
            ZipArchiveEntry questionEntry = archive.CreateEntry(StringResources.PATH_QUESTION_DIR + Path);
            using (Stream stream = questionEntry.Open())
            {
                // Serialize the question
                XmlSerializer x = new XmlSerializer(typeof(Question));
                x.Serialize(stream, question);
            }
        }
    }

    [Serializable()]
    public class Question
    {
        public String QuestionId;

        public QuestionType Type = QuestionType.TEXT;
        public String Text = "";
        public int ImageIndex = -1;
        public List<String> Answers = new List<String>();
        public int CorrectAnswer = -1;

        public override String ToString()
        {
            return QuestionId;
        }
    }

    [Serializable()]
    public enum QuestionType
    {
        TEXT, IMAGE
    }

    [Serializable()]
    public class ImageReference
    {
        public String Path = "";

        /// <summary>
        /// Loads an image from a zip archive
        /// </summary>
        /// <param name="archive">The archive to load from</param>
        /// <returns>The loaded image</returns>
        public Image LoadImage(ZipArchive archive)
        {
            Image image = null;
            // Open file in archive
            ZipArchiveEntry imageEntry = archive.GetEntry(StringResources.PATH_IMAGE_DIR + Path);
            using (Stream stream = imageEntry.Open())
            {
                // Load the image
                image = Image.FromStream(stream);
            }

            return image;
        }

        /// <summary>
        /// Save an image to zip archive
        /// </summary>
        /// <param name="image">The image to save</param>
        /// <param name="archive">The archive to save to</param>
        public void SaveImage(Image image, ZipArchive archive)
        {
            // Create file in zip
            ZipArchiveEntry imageEntry = archive.CreateEntry(StringResources.PATH_IMAGE_DIR + Path);
            using (Stream stream = imageEntry.Open())
            {
                // Save the image
                image.Save(stream, ImageFormat.Png);
            }
        }
    }
}

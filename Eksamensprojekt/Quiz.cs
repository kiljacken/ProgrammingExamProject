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

        [XmlIgnore]
        public List<Question> Questions = new List<Question>();

        [XmlIgnore]
        public List<Image> Images = new List<Image>();

        public Question AddQuestion()
        {
            Question question = new Question();
            question.QuestionId = "Question" + Questions.Count;

            Questions.Add(question);

            return question;
        }

        public void RemoveQuestion(Question question)
        {
            Questions.Remove(question);
        }

        public void RemoveImage(int index)
        {
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

        public static Quiz LoadFromZip(String path)
        {
            Quiz quiz = null;

            try
            {
                using (ZipArchive archive = ZipFile.Open(path, ZipArchiveMode.Read))
                {
                    ZipArchiveEntry quizEntry = archive.GetEntry("quiz.xml");

                    using (Stream stream = quizEntry.Open())
                    {
                        XmlSerializer x = new XmlSerializer(typeof(Quiz));
                        quiz = (Quiz)x.Deserialize(stream);
                    }

                    quiz.Questions = new List<Question>(quiz.QuestionReferences.Count);
                    for (int i = 0; i < quiz.QuestionReferences.Count; i++)
                    {
                        QuestionReference reference = quiz.QuestionReferences[i];
                        quiz.Questions.Add(reference.LoadQuestion(archive));
                    }

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
                MessageBox.Show("An error occured while loading the quiz:\n" + e.ToString());
                return null;
            }

            return quiz;
        }

        public static void StoreToZip(Quiz quiz, String path)
        {
            Boolean cleanup = false;
            string backup = path + ".bak";
            if (File.Exists(path))
            {
                if (File.Exists(backup))
                    File.Delete(backup);

                File.Move(path, backup);
                cleanup = true;
            }

            try
            {
                using (ZipArchive archive = ZipFile.Open(path, ZipArchiveMode.Create))
                {
                    quiz.ImageReferences.Clear();
                    for (int i = 0; i < quiz.Images.Count; i++)
                    {
                        Image image = quiz.Images[i];
                        ImageReference reference = new ImageReference { Path = "image" + i + ".png" };
                        quiz.ImageReferences.Add(reference);

                        reference.SaveImage(image, archive);
                    }

                    quiz.QuestionReferences.Clear();
                    for (int i = 0; i < quiz.Questions.Count; i++)
                    {
                        Question question = quiz.Questions[i];
                        QuestionReference reference = new QuestionReference { Path = "question" + i + ".xml" };
                        quiz.QuestionReferences.Add(reference);

                        reference.SaveQuestion(question, archive);
                    }

                    ZipArchiveEntry quizEntry = archive.CreateEntry("quiz.xml");
                    using (Stream stream = quizEntry.Open())
                    {
                        XmlSerializer x = new XmlSerializer(typeof(Quiz));
                        x.Serialize(stream, quiz);
                    }
                }
            }
            catch(Exception e)
            {
                if (cleanup && File.Exists(backup))
                {
                    File.Move(backup, path);
                    return;
                }

                MessageBox.Show("Error while saving quiz:\n" + e.ToString());
            }

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

        public Question LoadQuestion(ZipArchive archive)
        {
            Question question = null;
            using (Stream stream = archive.GetEntry(@"questions/" + Path).Open())
            {
                XmlSerializer x = new XmlSerializer(typeof(Question));
                question = (Question)x.Deserialize(stream);
            }

            return question;
        }

        public void SaveQuestion(Question question, ZipArchive archive)
        {
            ZipArchiveEntry questionEntry = archive.CreateEntry(@"questions/" + Path);

            using (Stream stream = questionEntry.Open())
            {
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

        public Image LoadImage(ZipArchive archive)
        {
            Image image = null;
            ZipArchiveEntry imageEntry = archive.GetEntry(@"images/" + Path);
            using (Stream stream = imageEntry.Open())
            {
                image = Image.FromStream(stream);
            }

            return image;
        }

        public void SaveImage(Image image, ZipArchive archive)
        {
            ZipArchiveEntry imageEntry = archive.CreateEntry(@"images/" + Path);
            using (Stream stream = imageEntry.Open())
            {
                image.Save(stream, ImageFormat.Png);
            }
        }
    }
}

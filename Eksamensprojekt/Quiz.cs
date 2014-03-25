using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Eksamensprojekt
{
    [Serializable()]
    class Quiz
    {
        public String Name;
        public String Description;
        public IList<QuestionReference> QuestionReferences;
        public IList<ImageReference> ImageReferences;

        [field: NonSerialized()]
        public IList<Question> Questions;

        [field: NonSerialized()]
        public IList<Image> Images;

        public static Quiz LoadFromZip(String path)
        {
            ZipArchive archive = ZipFile.Open(path, ZipArchiveMode.Read);
            ZipArchiveEntry quizEntry = archive.GetEntry("quiz.xml");

            Quiz quiz = null;
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

            return quiz;
        }

        public static void StoreToZip(Quiz quiz, String path)
        {
            ZipArchive archive = ZipFile.Open(path, ZipArchiveMode.Update);

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

    [Serializable()]
    class QuestionReference
    {
        public String Path;

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
    class Question
    {
        public QuestionType Type;
        public String Text;
        public int ImageIndex;
    }

    enum QuestionType
    {
        TEXT, IMAGE
    }

    [Serializable()]
    class ImageReference
    {
        public String Path;

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

using System;
using System.Collections.Generic;
using System.Drawing;
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
			    quiz.Questions.Add(Question.LoadFromReference(reference, archive));
			}

            quiz.Images = new List<Image>(quiz.ImageReferences.Count);
            for (int i = 0; i < quiz.ImageReferences.Count; i++)
			{
                ImageReference reference = quiz.ImageReferences[i];
                Image image = null;
			    using(Stream stream = archive.GetEntry(@"images/"+reference.Path).Open())
                {
                    image = Image.FromStream(stream);
                }

                quiz.Images.Add(image);
			}

            return quiz;
        }

    }

    [Serializable()]
    class QuestionReference
    {
        public String Path;
    }

    [Serializable()]
    class Question
    {
        public QuestionType Type;
        public String Value;

        public static Question LoadFromReference(QuestionReference reference, ZipArchive archive)
        {
            Question question = null;
            using(Stream stream = archive.GetEntry(@"questions/"+reference.Path).Open())
            {
                XmlSerializer x = new XmlSerializer(typeof(Question));
                question = (Question)x.Deserialize(stream);
            }
            
            return question;
        }
    }

    enum QuestionType
    {
        TEXT, IMAGE
    }

    [Serializable()]
    class ImageReference
    {
        public String Path;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eksamensprojekt
{
    class StringResources
    {
        public static String FILTER_QZI_DIALOG = "Quiz Files (.qzi)|*.qzi";
        public static String FILTER_IMAGE_DIALOG = "PNG File (.png)|*.png|" + "JPG File (.jpg)|*.jpg|" + "BMP File (.bmp)|*.bmp|" + "GIF File (.gif)|*.gif";
        
        public static String ERROR_TITLE = "Error";
        public static String ERROR_INVALID_QUESTION_TITLE = "Invalid question";
        public static String ERROR_INVALID_QUESTION_TEXT = "Please enter a question";
        public static String ERROR_INVALID_QUESTION_IMAGE = "Please select a valid image";
        public static String ERROR_INVALID_QUESTION_ANSWERS = "Please add at least one answer";
        public static String ERROR_INVALID_QUESTION_CORRECT_ANSWERS = "Please select a valid correct answer";
        public static String ERROR_NO_IMAGE_SELECTED = "Please select a image for the question!";
        public static String ERROR_OPEN_QUIZ = "Error while opening quiz";
        public static String ERROR_LOAD_QUIZ = "An error occured while loading the quiz:\n{0}";
        public static String ERROR_SAVE_QUIZ = "An error occured while saving the quiz:\n{0}";

        public static String DIALOG_ANSWER_INPUT_TITLE = "Answer creation";
        public static String DIALOG_ANSWER_INPUT_MESSAGE = "Input answer here";

        public static String MESSAGE_CORRECT_ANSWER = "Correct!";
        public static String MESSAGE_WRONG_ANSWER = "Wrong! The correct answer was: {0}";
        public static String MESSAGE_NUM_CORRECT_ANSWERS = "You had {0} correct answers";
        

        public static String PATH_QUIZ = "quiz.xml";
        public static String PATH_IMAGE = "image{0}.png";
        public static String PATH_IMAGE_DIR = "images/";
        public static String PATH_QUESTION = "question{0}.xml";
        public static String PATH_QUESTION_DIR = "questions/";

        public static String MISC_PICTURE_QUESTION_LABEL = "Picture question";
        public static String MISC_IMAGE_ID = "Image {0}";
        public static String MISC_QUESTION_ID = "Question {0}";
        public static String MISC_BACKUP_EXTENSION = ".bak";
    }
}

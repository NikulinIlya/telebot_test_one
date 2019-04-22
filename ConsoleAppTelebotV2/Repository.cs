using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppTelebotV2
{
    class Repository
    {
        static Repository()
        {
            BaseQuestions = new List<Question>()
            {
                new Question()
                {
                    TextQuestion = "Вопрос 1. 2 + 2 * 2",
                    Answer1 = "Ответ 4",
                    Answer2 = "Ответ 5",
                    Answer3 = "Ответ 6",
                    Answer4 = "Ответ 7",
                    RightAnswer = "Ответ 6"
                },

                new Question()
                {
                    TextQuestion = "Вопрос 2. 2 * 2",
                    Answer1 = "Ответ 4",
                    Answer2 = "Ответ 5",
                    Answer3 = "Ответ 6",
                    Answer4 = "Ответ 7",
                    RightAnswer = "Ответ 4"
                }
            };
        }

        /// <summary>
        /// Коллекция вопросов
        /// </summary>
        static public List<Question> BaseQuestions;
        
        /// <summary>
        /// Общее количество вопросов в репозитории
        /// </summary>
        public static int Count { get { return BaseQuestions.Count; } }
    }
}

﻿namespace Lab13.Models
{
    public class QuizModel
    {
        public static QuizModel instance { get; set; } = new QuizModel();

        public List<Expression> expressions { get; set; }

        public int rightAnswers { get; set; }

        public QuizModel()
        {
            expressions = new List<Expression> { };
        }

        public Expression addExpression()
        {
            Expression expresion = new Expression(expressions.Count + 1);
            expressions.Add(expresion);
            return expresion;
        }

        public Expression? findExpression(int id)
        {
            foreach (Expression exp in expressions)
            {
                if (exp.id == id)
                {
                    return exp;
                }
            }

            return null;
        }

        public void checkAnswer(int id, int userAnswer)
        {
            int? correctAnswer = null;

            foreach (Expression exp in expressions)
            {
                if (exp.id == id)
                {
                    correctAnswer = exp.answer;
                    exp.userAnswer = userAnswer;
                }
            }

            if (!(correctAnswer == null) && userAnswer == correctAnswer)
            {
                rightAnswers++;
            }
        }

        public bool isEmpty()
        {
            return expressions.Count == 0;
        }

        public void reset()
        {
            expressions = new List<Expression> { };
            rightAnswers = 0;
        }
    }
}

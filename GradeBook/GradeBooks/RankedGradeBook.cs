using GradeBook.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GradeBook.GradeBooks
{
    public class RankedGradeBook : BaseGradeBook
    {
        public RankedGradeBook(string name, bool isWeighted) : base (name, isWeighted)
        {
            Type = GradeBookType.Ranked;
        }

        public override char GetLetterGrade(double averageGrade)
        {
            if (Students.Count < 5)
            {
                throw new InvalidOperationException("Ranked-grading requires a minimum of 5 students to work");
            }

            // separando a lista de médias dos estudantes
            var avgGrades = new List<double>();
            foreach (var student in Students)
                avgGrades.Add(student.AverageGrade);

            // calcular percentis
            var percentiles = CalculatePercentile(20, avgGrades);                    

            // atribuir letra a nota
            switch(averageGrade)
            {
                case var avg when avg >= percentiles[3]:
                    return 'A';
                case var avg when avg >= percentiles[2]:
                    return 'B';
                case var avg when avg >= percentiles[1]:
                    return 'C';
                case var avg when avg >= percentiles[0]:
                    return 'D';
                default:
                    return 'F';
            }

            return 'a';
        }

        public double[] CalculatePercentile(int tamanhoPercentil, IEnumerable<double> valores)
        {
            var valoresOrdenados = valores.OrderBy(v => v).ToArray();

            var iteradorMaximo = 100 / tamanhoPercentil;

            var listaPercentis = new List<double>();

            for (var i = 1; i < iteradorMaximo; i++)
            {
                var posicaoPercentil = i * ((double)tamanhoPercentil / 100) * valoresOrdenados.Length;

                if (posicaoPercentil % 1 == 0) // verifica se a posição é um inteiro e faz a média de x(i) e x(i+1)
                    listaPercentis.Add((valoresOrdenados[(int)posicaoPercentil - 1] + valoresOrdenados[(int)posicaoPercentil]) / 2);
                else // se for decimal, arredonda pra baixo
                    listaPercentis.Add(valoresOrdenados[(int)Math.Floor(posicaoPercentil)]);
            }

            return listaPercentis.ToArray();

        }
        
        public override void CalculateStatistics()
        {
            if (Students.Count < 5)
                Console.WriteLine("Ranked grading requires at least 5 students with grades in order to properly calculate a student's overall grade.");
            else
                base.CalculateStatistics();
        }

        public override void CalculateStudentStatistics(string name)
        {
            if (Students.Count < 5)
                Console.WriteLine("Ranked grading requires at least 5 students with grades in order to properly calculate a student's overall grade.");
            else
                base.CalculateStudentStatistics(name);
        }

    }
}

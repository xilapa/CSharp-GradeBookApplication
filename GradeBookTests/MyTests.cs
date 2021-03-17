using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using GradeBook.GradeBooks;
//using FluentAssertions;

namespace GradeBookTests
{
    public class MyTests
    {

        //[Fact]
        public void RankedPercentil()
        {
            // Arrange
            var gradeBookRankeado = new RankedGradeBook("teste");
            var grades = new List<double> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            var tamanhoPercentil = 20;
            var percentisEsperados = new double[] {2.5,4.5,7,8.5 };

            // Act
            var result = gradeBookRankeado.CalculatePercentile(tamanhoPercentil, grades);

            // Assert
            //result.Should().BeEquivalentTo(percentisEsperados);
        }
    }
}

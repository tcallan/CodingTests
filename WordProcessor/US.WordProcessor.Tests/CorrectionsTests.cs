using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace US.WordProcessor.Tests
{
   [TestClass]
   public class CorrectionsTests
   {
      [TestMethod]
      public void ProperNounsSuffixedWithSNeedAnAposBeforeTheSCorrect()
      {
         var p = new Paragraph("Susan owns a hat. It is Susan’s hat.");
         var c = CorrectionFactory.CreateCorrectionFinder()
            .Find(p)
            .ToList();

         Assert.AreEqual(0, c.Count);
      }

      [TestMethod]
      public void ProperNounsSuffixedWithSNeedAnAposBeforeTheSIncorrect()
      {
         var p = new Paragraph("Susan owns a hat. It is Susans hat.");
         var c = CorrectionFactory.CreateCorrectionFinder()
            .Find(p)
            .Single();

         Assert.AreEqual(CorrectionType.OwnershipByAProperNoun, c.Type);
         Assert.AreEqual("It is Susans hat", c.Sentence);
         Assert.AreEqual("Susans", c.Word);
      }

      [TestMethod]
      public void ProperNounsPrecededByIsNeedAnAposBeforeTheSCorrect()
      {
         var p = new Paragraph("Barry owns a car. The car is Barry’s.");
         var c = CorrectionFactory.CreateCorrectionFinder()
            .Find(p)
            .ToList();

         Assert.AreEqual(0, c.Count);
      }

      [TestMethod]
      public void ProperNounsPrecededByIsNeedAnAposBeforeTheSIncorrect()
      {
         var p = new Paragraph("Barry owns a car. The car is Barrys.");
         var c = CorrectionFactory.CreateCorrectionFinder()
            .Find(p)
            .Single();

         Assert.AreEqual(CorrectionType.OwnershipByAProperNoun, c.Type);
         Assert.AreEqual("The car is Barrys", c.Sentence);
         Assert.AreEqual("Barrys", c.Word);
      }

      [TestMethod]
      public void RegularNounsDoNotNeedAnApostropheCorrect()
      {
         var p = new Paragraph("Look at those airplanes over there.");
         var c = CorrectionFactory.CreateCorrectionFinder()
            .Find(p)
            .ToList();

         Assert.AreEqual(0, c.Count);
      }

      [TestMethod]
      public void RegularNounsDoNotNeedAnApostropheIncorrect()
      {
         var p = new Paragraph("Look at those airplane's over there.");
         var c = CorrectionFactory.CreateCorrectionFinder()
            .Find(p)
            .Single();

         Assert.AreEqual(CorrectionType.IncorrectNounApostrophe, c.Type);
         Assert.AreEqual("Look at those airplane's over there", c.Sentence);
         Assert.AreEqual("airplane's", c.Word);
      }

      [TestMethod]
      public void IsntNeedsAnApostropheCorrect()
      {
         var p = new Paragraph("It isn't an airplane.");
         var c = CorrectionFactory.CreateCorrectionFinder()
            .Find(p)
            .ToList();

         Assert.AreEqual(0, c.Count);
      }

      [TestMethod]
      public void IsntNeedsAnApostropheIncorrect()
      {
         var p = new Paragraph("It isnt an airplane.");
         var c = CorrectionFactory.CreateCorrectionFinder()
            .Find(p)
            .Single();

         Assert.AreEqual(CorrectionType.MissingContractionApostrophe, c.Type);
         Assert.AreEqual("It isnt an airplane", c.Sentence);
         Assert.AreEqual("isnt", c.Word);
      }

      [TestMethod]
      public void IsntCapitalizedNeedsAnApostropheCorrect()
      {
         var p = new Paragraph("Isn't it an airplane?");
         var c = CorrectionFactory.CreateCorrectionFinder()
            .Find(p)
            .ToList();

         Assert.AreEqual(0, c.Count);
      }

      [TestMethod]
      public void IsntCaptializedNeedsAnApostropheIncorrect()
      {
         var p = new Paragraph("Isnt it an airplane?");
         var c = CorrectionFactory.CreateCorrectionFinder()
            .Find(p)
            .Single();

         Assert.AreEqual(CorrectionType.MissingContractionApostrophe, c.Type);
         Assert.AreEqual("Isnt it an airplane", c.Sentence);
         Assert.AreEqual("Isnt", c.Word);
      }

      [TestMethod]
      public void WontNeedsAnApostropheCorrect()
      {
         var p = new Paragraph("The airplane won't fly.");
         var c = CorrectionFactory.CreateCorrectionFinder()
            .Find(p)
            .ToList();

         Assert.AreEqual(0, c.Count);
      }

      [TestMethod]
      public void WontNeedsAnApostropheIncorrect()
      {
         var p = new Paragraph("The airplane wont fly.");
         var c = CorrectionFactory.CreateCorrectionFinder()
            .Find(p)
            .Single();

         Assert.AreEqual(CorrectionType.MissingContractionApostrophe, c.Type);
         Assert.AreEqual("The airplane wont fly", c.Sentence);
         Assert.AreEqual("wont", c.Word);
      }

      [TestMethod]
      public void DoesntNeedsAnApostropheCorrect()
      {
         var p = new Paragraph("The airplane doesn't fly.");
         var c = CorrectionFactory.CreateCorrectionFinder()
            .Find(p)
            .ToList();

         Assert.AreEqual(0, c.Count);
      }

      [TestMethod]
      public void DoesntNeedsAnApostropheIncorrect()
      {
         var p = new Paragraph("The airplane doesnt fly.");
         var c = CorrectionFactory.CreateCorrectionFinder()
            .Find(p)
            .Single();

         Assert.AreEqual(CorrectionType.MissingContractionApostrophe, c.Type);
         Assert.AreEqual("The airplane doesnt fly", c.Sentence);
         Assert.AreEqual("doesnt", c.Word);
      }
   }
}

using System;

namespace US.WordProcessor
{
   public class Correction : IEquatable<Correction>
   {
      public Correction(CorrectionType type, string sentence, string word)
      {
         Type = type;
         Sentence = sentence;
         Word = word;
      }

      public readonly CorrectionType Type;
      public readonly string Sentence;
      public readonly string Word;

      public bool Equals(Correction other)
      {
         return Type.Equals(other.Type)
                && Sentence.Equals(other.Sentence)
                && Word.Equals(other.Word);
      }

      public override int GetHashCode()
      {
         return Type.GetHashCode() ^ Sentence.GetHashCode() ^ Word.GetHashCode();
      }
   }
}
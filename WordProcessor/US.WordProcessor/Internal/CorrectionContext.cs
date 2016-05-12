
namespace US.WordProcessor.Internal
{
   internal class CorrectionContext
   {
      public CorrectionContext(Optional<Definition> prev, Definition current, Optional<Definition> next, string word,
         string sentence)
      {
         Prev = prev;
         Current = current;
         Next = next;
         Word = word;
         Sentence = sentence;
      }

      public readonly Optional<Definition> Prev;
      public readonly Definition Current;
      public readonly Optional<Definition> Next;
      public readonly string Word;
      public readonly string Sentence;
   }
}

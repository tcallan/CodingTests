using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace US.WordProcessor.Internal
{
   internal static class Rules
   {
      public static Optional<Correction> ProperNounWithSFollowedByNounNeedsApostrophe(CorrectionContext context)
      {
         if (context.Current.Type == WordType.ProperNoun
            && context.Current.Suffix == "s"
            && context.Next.Map(next => next.Type == WordType.Noun).OrElse(false))
         {
            return Optional.Of(new Correction(CorrectionType.OwnershipByAProperNoun, context.Sentence, context.Word));
         }
         return Optional.Empty<Correction>();
      }

      public static Optional<Correction> ProperNounWithSPreceededByIsNeedsApostrophe(CorrectionContext context)
      {
         if (context.Current.Type == WordType.ProperNoun
            && context.Current.Suffix.Equals("s")
            && context.Prev.Map(prev => prev.Word == "is").OrElse(false))
         {
            return Optional.Of(new Correction(CorrectionType.OwnershipByAProperNoun, context.Sentence, context.Word));
         }
         return Optional.Empty<Correction>();
      }

      public static Optional<Correction> ProperNounDoesNotNeedApostrophe(CorrectionContext context)
      {
         if (context.Current.Type == WordType.Noun
            && context.Word.EndsWith("'s"))
         {
            return Optional.Of(new Correction(CorrectionType.IncorrectNounApostrophe, context.Sentence, context.Word));
         }
         return Optional.Empty<Correction>();
      }

      private static readonly ICollection<string> NeedsApostrophe =
         new HashSet<string>(StringComparer.CurrentCultureIgnoreCase)
         {
            "doesnt",
            "isnt",
            "wont"
         };

      public static Optional<Correction> ContractionNeedsApostrophe(CorrectionContext context)
      {
         return NeedsApostrophe.Contains(context.Word)
            ? Optional.Of(new Correction(CorrectionType.MissingContractionApostrophe, context.Sentence, context.Word))
            : Optional.Empty<Correction>();
      }
   }
}

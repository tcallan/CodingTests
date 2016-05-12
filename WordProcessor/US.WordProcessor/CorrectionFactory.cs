using System;
using System.Collections.Generic;
using US.WordProcessor.Internal;

namespace US.WordProcessor
{
   public static class CorrectionFactory
   {
      public static ICorrectionFinder CreateCorrectionFinder()
      {
         return new CorrectionFinder(
            new List<Func<CorrectionContext, Optional<Correction>>>
            {
               Rules.ProperNounWithSFollowedByNounNeedsApostrophe,
               Rules.ProperNounWithSPreceededByIsNeedsApostrophe,
               Rules.ProperNounDoesNotNeedApostrophe,
               Rules.ContractionNeedsApostrophe
            },
            new Dictionary());
      }
   }
}

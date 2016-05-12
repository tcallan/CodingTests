using System;
using System.Collections.Generic;
using System.Linq;
using US.WordProcessor.Internal;

namespace US.WordProcessor
{
   internal class CorrectionFinder
      : ICorrectionFinder
   {
      private readonly IEnumerable<Func<CorrectionContext, Optional<Correction>>> _rules;
      private readonly Dictionary _dictionary;

      public CorrectionFinder(IEnumerable<Func<CorrectionContext, Optional<Correction>>> rules, Dictionary dictionary)
      {
         _rules = rules;
         _dictionary = dictionary;
      }

      public IEnumerable<Correction> Find(Paragraph paragraph)
      {
         return paragraph.SelectMany(sentence =>
         {
            return
               sentence.Select(word => new { Definition = _dictionary.Define(word), Word = word })
                  .SelectTuplewise((prev, current, next) => new CorrectionContext(
                      prev.Map(p => p.Definition),
                      current.Definition,
                      next.Map(n => n.Definition),
                      current.Word,
                      sentence.ToString())
                  )
                  .Select(GatherCorrections)
                  .SelectMany(corrections => corrections)
                  .Distinct();
         });
      }

      private IEnumerable<Correction> GatherCorrections(CorrectionContext context)
      {
         return
            _rules.Select(func => func(context))
               .Where(maybeCorrection => maybeCorrection.HasValue)
               .Select(correction => correction.Value);
      }
   }
}

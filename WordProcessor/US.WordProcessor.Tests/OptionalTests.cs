using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using US.WordProcessor.Internal;

namespace US.WordProcessor.Tests
{
   [TestClass]
   public class OptionalTests
   {
      [TestMethod]
      public void OptionalFromNullSetsHasValueProperly()
      {
         var optional = new Optional<string>(null);
         Assert.IsFalse(optional.HasValue);
      }

      [TestMethod]
      public void OptionalFromNonNullSetsHasValueProperly()
      {
         var optional = new Optional<string>("foo");
         Assert.IsTrue(optional.HasValue);
      }

      [TestMethod]
      public void OrElseReturnsRightThingForEmptyOptional()
      {
         var optional = Optional.Empty<string>();
         Assert.AreEqual("foo", optional.OrElse("foo"));
      }

      [TestMethod]
      public void OrElseReturnsRightThingForNonEmptyOptional()
      {
         var optional = Optional.Of("foo");
         Assert.AreEqual("foo", optional.OrElse("bar"));
      }

      [TestMethod]
      public void MapDoesNotCallFunctionForEmptyOptional()
      {
         var optional = Optional.Empty<string>();
         var mapped = optional.Map((value) =>
         {
            Assert.IsFalse(true, "function was called");
            return value;
         });

         Assert.IsFalse(mapped.HasValue);
      }

      [TestMethod]
      public void MapCallsFunctionForNonEmptyOptional()
      {
         var optional = Optional.Of("foo");
         var mapped = optional.Map((value) =>
         {
            Assert.AreEqual("foo", value);
            return "bar";
         });

         Assert.IsTrue(mapped.HasValue);
         Assert.AreEqual("bar", mapped.Value);
      }
   }
}

using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using US.WordProcessor.Internal;

namespace US.WordProcessor.Tests
{
   [TestClass]
   public class EnumerableExtensionsTest
   {
      private class Window<T>
      {
         public Optional<T> Prev;
         public T Current;
         public Optional<T> Next;
      }

      private static Window<T> AsWindow<T>(Optional<T> prev, T current, Optional<T> next)
      {
         return new Window<T>()
         {
            Prev = prev,
            Current = current,
            Next = next
         };
      }

      [TestMethod]
      public void EnumerableSize1()
      {
         var list = new[] { "a" };
         var tuples = list.SelectTuplewise(AsWindow).ToList();

         Assert.AreEqual(1, tuples.Count);
         var tuple = tuples.First();
         Assert.IsFalse(tuple.Prev.HasValue, "previous item has a value");
         Assert.IsFalse(tuple.Next.HasValue, "next item has a value");
         Assert.AreEqual("a", tuple.Current);
      }

      [TestMethod]
      public void EnumerableSize2()
      {
         var list = new[] { "a", "b" };
         var tuples = list.SelectTuplewise(AsWindow).ToList();

         Assert.AreEqual(2, tuples.Count);

         var first = tuples.First();
         Assert.IsFalse(first.Prev.HasValue, "previous item has a value");
         Assert.IsTrue(first.Next.HasValue, "next item doesn't have a value");
         Assert.AreEqual("b", first.Next.Value);
         Assert.AreEqual("a", first.Current);

         var second = tuples[1];
         Assert.IsTrue(second.Prev.HasValue);
         Assert.AreEqual("a", second.Prev.Value);
         Assert.AreEqual("b", second.Current);
         Assert.IsFalse(second.Next.HasValue);
      }

      [TestMethod]
      public void EnumerableSize3()
      {
         var list = new[] { "a", "b", "c" };
         var tuples = list.SelectTuplewise(AsWindow).ToList();

         Assert.AreEqual(3, tuples.Count);

         var first = tuples.First();
         Assert.IsFalse(first.Prev.HasValue, "previous item has a value");
         Assert.IsTrue(first.Next.HasValue, "next item doesn't have a value");
         Assert.AreEqual("b", first.Next.Value);
         Assert.AreEqual("a", first.Current);

         var second = tuples[1];
         Assert.IsTrue(second.Prev.HasValue);
         Assert.AreEqual("a", second.Prev.Value);
         Assert.AreEqual("b", second.Current);
         Assert.IsTrue(second.Next.HasValue);
         Assert.AreEqual("c", second.Next.Value);

         var third = tuples[2];
         Assert.IsTrue(third.Prev.HasValue);
         Assert.AreEqual("b", third.Prev.Value);
         Assert.AreEqual("c", third.Current);
         Assert.IsFalse(third.Next.HasValue);
      }
   }
}

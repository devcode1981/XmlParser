﻿using Microsoft.Language.Xml.Editor;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Microsoft.Language.Xml.Test
{
    [TestClass]
    public class TestSmartIndent
    {
        [TestMethod]
        public void TestIndent1()
        {
            T(@"<x>
    |
</x>");
            T(@"<x>
    |
</x>
");
            T(@"<x>
    <x>
        |
    </x>
</x>");
            T(@"<x>
    <x>
        <x>
            |
        </x>
    </x>
</x>");
            T(@"<X>
    |
    <a/>
    <a/>
</X>");
            T(@"<x>
</x>
|
");
        }

        [TestMethod]
        public void TestIndent2()
        {
            T(@"<x>
</x>
|
");
        }

        private void T(string xmlWithCaret)
        {
            int caret = xmlWithCaret.IndexOf('|');
            int expectedIndent = 0;
            while (caret - expectedIndent > 1 && xmlWithCaret[caret - expectedIndent - 1] == ' ')
            {
                expectedIndent++;
            }

            var xml = xmlWithCaret.Remove(caret, 1);
            var root = Parser.ParseText(xml);
            var actualIndent = SmartIndent.FindTotalParentChainIndent(root, caret, 0, 0);
            Assert.AreEqual(expectedIndent, actualIndent);
        }
    }
}
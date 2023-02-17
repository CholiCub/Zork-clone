//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using Microsoft.VisualStudio.TestTools.UnitTesting;


//namespace Zork.Tests
//{
//    [TestClass]
//    public class CommandTest
//    {
//        [TestMethod]
//        public void TestConstructor()
//        {
//            const string name = "Test Command Name";
//            IEnumerable<string> verbs = new string[]{"UP"};
            
//            Action<CommandContext> action = (cc) => { };

//            var c = new Command(name, verbs, action);

//            Assert.AreEqual(name.Trim().ToUpper(), c.Name);
//            Assert.AreEqual(verbs.Count(), c.Verbs.Count());
//            Assert.AreEqual(verbs.First(), c.Verbs.First());
//            Assert.AreEqual(action, c.Action);        
//        }

//        [TestMethod]
//        public void TestVerbConstructor()
//        {
//            string verb = "UP";

//            var c = new Command("TEST", verb, (cc) => { });

//            Assert.AreEqual(1, c.Verbs.Count());
//            Assert.AreEqual(verb, c.Verbs.First());
//        }
//    }
//}

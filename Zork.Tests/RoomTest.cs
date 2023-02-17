//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using System;
//using System.Collections.Generic;

//namespace Zork.Tests
//{
//    [TestClass]
//    public class RoomTest
//    {

//        [TestMethod]
//        public void TestConstructor()
//        {
//            const string name = "West of House";
//            const string description = "A description";
//            {
//                Room room = new Room(name, description);
//                Assert.AreEqual(name, room.Name);
//                Assert.AreEqual(description, room.Description);
//            }
//        }
//        [TestMethod]
//        public void TestUpdateNeighbors()
//        {
//            var roomA = new Room("A");
//            roomA.NeighborName = new Dictionary<Directions, string>
//            {
//                { Directions.DOWN, "B" },
//                { Directions.UP, "C" }
//            };

//            var world = new World();
//            var roomB = new Room("B");
//            var roomC = new Room("C");
//            world.RoomsByName = new Dictionary<string, Room>
//            {
//                { "A", roomA },
//                { "B", roomB },
//                { "C", roomC }
//            };

//            roomA.UpdateNeighbors(world);

//            Assert.AreSame(roomB, roomA.Neighbors[Directions.DOWN]);
//            Assert.AreSame(roomC, roomA.Neighbors[Directions.UP]);
//        }
//        [TestMethod]
//        public void TestToString()
//        {
//            string loc = "East of House";
//            var p = new Room(loc);
//            Assert.AreEqual(loc, p.ToString());
            
//        }
//    }
//}

//using System;
//using System.Collections.Generic;
//using System.Text;
//using Microsoft.VisualStudio.TestTools.UnitTesting;

//namespace Zork.Tests
//{
//    [TestClass]
//    public class PlayerTest
//    {
//        [TestMethod]
//        public void TestConstructor()
//        {
//            string startLoc = "Up a Tree";
//            var room = new Room(startLoc);
//            var w = new World();
//            w.RoomsByName = new Dictionary<string, Room>
//            {
//                { startLoc , room }
//            };
//            var p = new Player(w, startLoc);
//            Assert.AreEqual(p.CurrentRoom, room);
//        }

//        [TestMethod]
//        public void TestMove()
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

//            var player = new Player(world, "A");
//            Assert.AreSame(roomA, player.CurrentRoom);
//            Assert.IsNull(player.PreviousRoom);
//            player.Move(Directions.DOWN);
//            Assert.AreSame(roomB, player.CurrentRoom);
//            Assert.AreSame(roomA, player.PreviousRoom);
//        }

//        [TestMethod]
//        public void TestInvlaidMove()
//        {
//            var roomA = new Room("A");
//            roomA.NeighborName = new Dictionary<Directions, string>
//            {
//            };

//            var world = new World();
//            world.RoomsByName = new Dictionary<string, Room>
//            {
//                { "A", roomA }
//            };

//            roomA.UpdateNeighbors(world);

//            var player = new Player(world, "A");
//            Assert.AreSame(roomA, player.CurrentRoom);
//            Assert.IsNull(player.PreviousRoom);
//            player.Move(Directions.DOWN);
//            Assert.AreSame(roomA, player.CurrentRoom);
//            Assert.IsNull(player.PreviousRoom);
//        }
//    }
//}

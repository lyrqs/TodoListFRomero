using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TodoList.Domain.Models;

namespace TodoList.Test
{
    [TestClass]
    public class TodoItemTests
    {
        [TestMethod]
        public void Can_Create_TodoItem_And_Add_Valid_Progressions()
        {
            // Arrange
            var item = new TodoItem(1, "Test Task", "Description", "Work");
            var prog1 = new Progression(new DateTime(2025, 3, 18), 30);
            var prog2 = new Progression(new DateTime(2025, 3, 19), 50);
            var prog3 = new Progression(new DateTime(2025, 3, 20), 20);

            // Act
            item.AddProgression(prog1);
            item.AddProgression(prog2);
            item.AddProgression(prog3);

            // Assert
            Assert.AreEqual(3, item.Progressions.Count);
            Assert.IsTrue(item.IsCompleted);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Progression_With_Invalid_Percent_Should_Throw()
        {
            var invalid = new Progression(DateTime.Now, 150);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Adding_Progression_With_Lower_Date_Should_Throw()
        {
            var item = new TodoItem(1, "Task", "Desc", "Work");
            item.AddProgression(new Progression(new DateTime(2025, 3, 19), 40));
            item.AddProgression(new Progression(new DateTime(2025, 3, 18), 30)); // Forced error: Invalid
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Adding_Progression_That_Exceeds_100_Should_Throw()
        {
            var item = new TodoItem(1, "Task", "Desc", "Work");
            item.AddProgression(new Progression(DateTime.Now, 60));
            item.AddProgression(new Progression(DateTime.Now.AddDays(1), 50)); // Forced error: Exceeds 100
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Updating_Description_Over_50Percent_Should_Throw()
        {
            var item = new TodoItem(1, "Task", "Desc", "Work");
            item.AddProgression(new Progression(DateTime.Now, 60));
            item.UpdateDescription("New description"); // Forced error: Not allowed
        }

        [TestMethod]
        public void Updating_Description_Under_50Percent_Should_Work()
        {
            var item = new TodoItem(1, "Task", "Desc", "Work");
            item.AddProgression(new Progression(DateTime.Now, 40));
            item.UpdateDescription("Updated");
            Assert.AreEqual("Updated", item.Description);
        }
    }
}

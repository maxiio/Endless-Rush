using NUnit.Framework;
using static UI.GameOver.GameOverScoreDisplay;

namespace Editor {
	public class ScoreReplacement {
		[Test]
		public void ScoreReplacementSimplePasses() {
			Assert.AreEqual(ReplaceNumberForNewNumber("SCORE\n0", "0"), "SCORE\n0");
			Assert.AreEqual(ReplaceNumberForNewNumber("SCORE\n5", "5"), "SCORE\n5");
			Assert.AreEqual(ReplaceNumberForNewNumber("SCORE\naAxw", "0"), "SCORE\naAxw");
			Assert.AreEqual(ReplaceNumberForNewNumber("Line", "1"), "Line");
			Assert.AreEqual(ReplaceNumberForNewNumber("", "1"), "");

			Assert.AreEqual(ReplaceNumberForNewNumber("SCORE\n0", "1"), "SCORE\n1");
			Assert.AreEqual(ReplaceNumberForNewNumber("SCORE\n1", "1"), "SCORE\n1");
			Assert.AreEqual(ReplaceNumberForNewNumber("SCORE\n1", "2"), "SCORE\n2");
			Assert.AreEqual(ReplaceNumberForNewNumber("SCORE\n10", "12"), "SCORE\n12");

			Assert.AreEqual(ReplaceNumberForNewNumber("SCORE\n234", "3"), "SCORE\n3");
			Assert.AreEqual(ReplaceNumberForNewNumber("SCORE\n0789", "9"), "SCORE\n9");
			
			Assert.AreEqual(ReplaceNumberForNewNumber("SCORE\n123Text45", "0"), "SCORE\n0Text0");
			Assert.AreEqual(ReplaceNumberForNewNumber("SCORE\n123Text45qwe4", "123"), "SCORE\n123Text123qwe123");
		}
	}
}
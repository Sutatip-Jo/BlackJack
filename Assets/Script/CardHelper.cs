public enum CardSuits
{
    Spade = 1,
    Heart = 2,
    Diamond = 3,
    Club = 4
}
public enum CardRanks
{
    Ace = 1,
    Two = 2,
    Three = 3,
    Four = 4,
    Five = 5,
    Six = 6,
    Seven = 7,
    Eight = 8,
    Nine = 9,
    Ten = 10,
    Jack = 11,
    Queen = 12,
    King = 13
}
public static class CardHelper
{
    public static int GetCardId(CardSuits face, CardRanks score)
    {
        return ((int)face * 100) + (int)score;
    }

    public static string GetRankToShort(CardRanks rank)
    {
        switch (rank)
        {
            case CardRanks.Ace: return "A";
            case CardRanks.Two: return "2";
            case CardRanks.Three: return "3";
            case CardRanks.Four: return "4";
            case CardRanks.Five: return "5";
            case CardRanks.Six: return "6";
            case CardRanks.Seven: return "7";
            case CardRanks.Eight: return "8";
            case CardRanks.Nine: return "9";
            case CardRanks.Ten: return "10";
            case CardRanks.Jack: return "J";
            case CardRanks.Queen: return "Q";
            case CardRanks.King: return "K";
            default: return string.Empty;
        }
    }
}
public enum cardFaces
{
    Spade = 1,
    Heart = 2,
    Diamond = 3,
    Club = 4
}
public enum cardScore
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
    public static int GetCardId(cardFaces face, cardScore score)
    {
        return ((int)face * 100) + (int)score;
    }
}
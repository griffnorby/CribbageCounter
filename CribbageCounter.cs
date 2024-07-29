using System.Text.RegularExpressions;

namespace CribbageCounter
{
    public class CribbageCounter
    {
        public static void Main(string[] args)
        {
            
        }
    }

    namespace Cards
    {
        public sealed record Denomination(string Identifier, string Name, int SequentialValue, int ArithmeticValue) : IComparable<Denomination>, IEquatable<string>
        {
            public static readonly Denomination Ace = new("A", "Ace", 1, 1);
            public static readonly Denomination Two = new("2", "Two", 2, 2);
            public static readonly Denomination Three = new("3", "Three", 3, 3);
            public static readonly Denomination Four = new("4", "Four", 4, 4);
            public static readonly Denomination Five = new("5", "Five", 5, 5);
            public static readonly Denomination Six = new("6", "Six", 6, 6);
            public static readonly Denomination Seven = new("7", "Seven", 7, 7);
            public static readonly Denomination Eight = new("8", "Eight", 8, 8);
            public static readonly Denomination Nine = new("9", "Nine", 9, 9);
            public static readonly Denomination Ten = new("10", "Ten", 10, 10);
            public static readonly Denomination Jack = new("J", "Jack", 11, 10);
            public static readonly Denomination Queen = new("Q", "Queen", 12, 10);
            public static readonly Denomination King = new("K", "King", 13, 10);

            public int CompareTo(Denomination? other) => other is null ? 1 : SequentialValue.CompareTo(other.SequentialValue);

            public bool Precedes(Denomination? other) => CompareTo(other) < 0;

            public bool IsPrevious(Denomination? other) => other is not null && other.SequentialValue - SequentialValue == 1;

            public bool Follows(Denomination? other) => CompareTo(other) > 0;

            public bool IsNext(Denomination? other) => other is not null && SequentialValue - other.SequentialValue == 1;

            public bool Equals(string? other) => other is not null && (Identifier.Equals(other, StringComparison.OrdinalIgnoreCase) || Name.Equals(other, StringComparison.OrdinalIgnoreCase));

            public override string ToString() => Name;

            public override int GetHashCode() => (Identifier, ArithmeticValue, SequentialValue).GetHashCode();

            public static bool operator <(Denomination? left, Denomination? right) => left is not null && left.Precedes(right);

            public static bool operator <=(Denomination? left, Denomination? right) => left < right || left == right;

            public static bool operator >(Denomination? left, Denomination? right) => left is not null && left.Follows(right);

            public static bool operator >=(Denomination? left, Denomination? right) => left > right || left == right;

            public static Denomination Parse(string? str) => str switch
            {
                _ when Ace.Equals(str) => Ace,
                _ when Two.Equals(str) => Two,
                _ when Three.Equals(str) => Three,
                _ when Four.Equals(str) => Four,
                _ when Five.Equals(str) => Five,
                _ when Six.Equals(str) => Six,
                _ when Seven.Equals(str) => Seven,
                _ when Eight.Equals(str) => Eight,
                _ when Nine.Equals(str) => Nine,
                _ when Ten.Equals(str) => Ten,
                _ when Jack.Equals(str) => Jack,
                _ when Queen.Equals(str) => Queen,
                _ when King.Equals(str) => King,
                _ => throw new ArgumentException($"{str} is not a valid Denomination", nameof(str))
            };
        }

        public sealed record Suit(string Identifier, string Name) : IComparable<Suit>, IEquatable<string>
        {
            public static readonly Suit Clubs = new("C", "Clubs");
            public static readonly Suit Diamonds = new("D", "Diamonds");
            public static readonly Suit Hearts = new("H", "Hearts");
            public static readonly Suit Spades = new("S", "Spades");

            public int CompareTo(Suit? other) => other is not null ? Identifier.CompareTo(other.Identifier) : 1;

            public bool Precedes(Suit? other) => CompareTo(other) < 0;

            public bool Follows(Suit? other) => CompareTo(other) > 0;

            public bool Equals(string? other) => other is not null && (Identifier.Equals(other, StringComparison.OrdinalIgnoreCase) || Name.Equals(other, StringComparison.OrdinalIgnoreCase));

            public override string ToString() => Name;

            public override int GetHashCode() => Identifier.GetHashCode();

            public static bool operator <(Suit? left, Suit? right) => left is not null && left.Precedes(right);

            public static bool operator <=(Suit? left, Suit? right) => left < right || left == right;

            public static bool operator >(Suit? left, Suit? right) => left is not null && left.Follows(right);

            public static bool operator >=(Suit? left, Suit? right) => left > right || left == right;

            public static Suit Parse(string? str) => str switch
            {
                _ when Clubs.Equals(str) => Clubs,
                _ when Diamonds.Equals(str) => Diamonds,
                _ when Hearts.Equals(str) => Hearts,
                _ when Spades.Equals(str) => Spades,
                _ => throw new ArgumentException($"{str} is not a valid Suit", nameof(str))
            };
        }

        public sealed partial record Card(Denomination Denomination, Suit Suit) : IComparable<Card>, IEquatable<Tuple<string, string>>, IEquatable<string>
        {
            public static readonly Card AceOfClubs = new(Denomination.Ace, Suit.Clubs);
            public static readonly Card TwoOfClubs = new(Denomination.Two, Suit.Clubs);
            public static readonly Card ThreeOfClubs = new(Denomination.Three, Suit.Clubs);
            public static readonly Card FourOfClubs = new(Denomination.Four, Suit.Clubs);
            public static readonly Card FiveOfClubs = new(Denomination.Five, Suit.Clubs);
            public static readonly Card SixOfClubs = new(Denomination.Six, Suit.Clubs);
            public static readonly Card SevenOfClubs = new(Denomination.Seven, Suit.Clubs);
            public static readonly Card EightOfClubs = new(Denomination.Eight, Suit.Clubs);
            public static readonly Card NineOfClubs = new(Denomination.Nine, Suit.Clubs);
            public static readonly Card TenOfClubs = new(Denomination.Ten, Suit.Clubs);
            public static readonly Card JackOfClubs = new(Denomination.Jack, Suit.Clubs);
            public static readonly Card QueenOfClubs = new(Denomination.Queen, Suit.Clubs);
            public static readonly Card KingOfClubs = new(Denomination.King, Suit.Clubs);
            public static readonly Card AceOfDiamonds = new(Denomination.Ace, Suit.Diamonds);
            public static readonly Card TwoOfDiamonds = new(Denomination.Two, Suit.Diamonds);
            public static readonly Card ThreeOfDiamonds = new(Denomination.Three, Suit.Diamonds);
            public static readonly Card FourOfDiamonds = new(Denomination.Four, Suit.Diamonds);
            public static readonly Card FiveOfDiamonds = new(Denomination.Five, Suit.Diamonds);
            public static readonly Card SixOfDiamonds = new(Denomination.Six, Suit.Diamonds);
            public static readonly Card SevenOfDiamonds = new(Denomination.Seven, Suit.Diamonds);
            public static readonly Card EightOfDiamonds = new(Denomination.Eight, Suit.Diamonds);
            public static readonly Card NineOfDiamonds = new(Denomination.Nine, Suit.Diamonds);
            public static readonly Card TenOfDiamonds = new(Denomination.Ten, Suit.Diamonds);
            public static readonly Card JackOfDiamonds = new(Denomination.Jack, Suit.Diamonds);
            public static readonly Card QueenOfDiamonds = new(Denomination.Queen, Suit.Diamonds);
            public static readonly Card KingOfDiamonds = new(Denomination.King, Suit.Diamonds);
            public static readonly Card AceOfHearts = new(Denomination.Ace, Suit.Hearts);
            public static readonly Card TwoOfHearts = new(Denomination.Two, Suit.Hearts);
            public static readonly Card ThreeOfHearts = new(Denomination.Three, Suit.Hearts);
            public static readonly Card FourOfHearts = new(Denomination.Four, Suit.Hearts);
            public static readonly Card FiveOfHearts = new(Denomination.Five, Suit.Hearts);
            public static readonly Card SixOfHearts = new(Denomination.Six, Suit.Hearts);
            public static readonly Card SevenOfHearts = new(Denomination.Seven, Suit.Hearts);
            public static readonly Card EightOfHearts = new(Denomination.Eight, Suit.Hearts);
            public static readonly Card NineOfHearts = new(Denomination.Nine, Suit.Hearts);
            public static readonly Card TenOfHearts = new(Denomination.Ten, Suit.Hearts);
            public static readonly Card JackOfHearts = new(Denomination.Jack, Suit.Hearts);
            public static readonly Card QueenOfHearts = new(Denomination.Queen, Suit.Hearts);
            public static readonly Card KingOfHearts = new(Denomination.King, Suit.Hearts);
            public static readonly Card AceOfSpades = new(Denomination.Ace, Suit.Spades);
            public static readonly Card TwoOfSpades = new(Denomination.Two, Suit.Spades);
            public static readonly Card ThreeOfSpades = new(Denomination.Three, Suit.Spades);
            public static readonly Card FourOfSpades = new(Denomination.Four, Suit.Spades);
            public static readonly Card FiveOfSpades = new(Denomination.Five, Suit.Spades);
            public static readonly Card SixOfSpades = new(Denomination.Six, Suit.Spades);
            public static readonly Card SevenOfSpades = new(Denomination.Seven, Suit.Spades);
            public static readonly Card EightOfSpades = new(Denomination.Eight, Suit.Spades);
            public static readonly Card NineOfSpades = new(Denomination.Nine, Suit.Spades);
            public static readonly Card TenOfSpades = new(Denomination.Ten, Suit.Spades);
            public static readonly Card JackOfSpades = new(Denomination.Jack, Suit.Spades);
            public static readonly Card QueenOfSpades = new(Denomination.Queen, Suit.Spades);
            public static readonly Card KingOfSpades = new(Denomination.King, Suit.Spades);

            [GeneratedRegex(
                "^(?<denomination>[AJQK]|[2-9]|10|Ace|Two|Three|Four|Five|Six|Seven|Eight|Nine|Ten|Jack|Queen|King)(?:\\s?of\\s?)?(?<suit>[CDHS]|Clubs|Diamonds|Hearts|Spades)$",
                RegexOptions.IgnoreCase | RegexOptions.Compiled,
                "en-US")]
            private static partial Regex Regex();

            public string Identifier => Denomination.Identifier + Suit.Identifier;
            public string Name => Denomination.Name + " of " + Suit.Name;

            public int CompareTo(Card? other) => other is not null ? (Denomination, Suit).CompareTo((other.Denomination, other.Suit)) : 1;

            public bool Precedes(Card? other) => CompareTo(other) < 0;

            public bool Follows(Card? other) => CompareTo(other) > 0;

            public bool Equals(Tuple<string, string>? other) => other is not null && Denomination.Equals(other.Item1) && Suit.Equals(other.Item2);

            public bool Equals(string? other)
            {
                if(other is null) return false;

                Match match = Regex().Match(other);

                if(!match.Success) return false;

                return Equals(Tuple.Create(match.Groups["denomination"].Value, match.Groups["suit"].Value));
            }

            public override string ToString() => Name;

            public override int GetHashCode() => (Denomination, Suit).GetHashCode();

            public static Card Get(Denomination denomination, Suit suit) => (denomination, suit) switch
            {
                _ when denomination == Denomination.Ace && suit == Suit.Clubs => AceOfClubs,
                _ when denomination == Denomination.Two && suit == Suit.Clubs => TwoOfClubs,
                _ when denomination == Denomination.Three && suit == Suit.Clubs => ThreeOfClubs,
                _ when denomination == Denomination.Four && suit == Suit.Clubs => FourOfClubs,
                _ when denomination == Denomination.Five && suit == Suit.Clubs => FiveOfClubs,
                _ when denomination == Denomination.Six && suit == Suit.Clubs => SixOfClubs,
                _ when denomination == Denomination.Seven && suit == Suit.Clubs => SevenOfClubs,
                _ when denomination == Denomination.Eight && suit == Suit.Clubs => EightOfClubs,
                _ when denomination == Denomination.Nine && suit == Suit.Clubs => NineOfClubs,
                _ when denomination == Denomination.Ten && suit == Suit.Clubs => TenOfClubs,
                _ when denomination == Denomination.Jack && suit == Suit.Clubs => JackOfClubs,
                _ when denomination == Denomination.Queen && suit == Suit.Clubs => QueenOfClubs,
                _ when denomination == Denomination.King && suit == Suit.Clubs => KingOfClubs,
                _ when denomination == Denomination.Ace && suit == Suit.Diamonds => AceOfDiamonds,
                _ when denomination == Denomination.Two && suit == Suit.Diamonds => TwoOfDiamonds,
                _ when denomination == Denomination.Three && suit == Suit.Diamonds => ThreeOfDiamonds,
                _ when denomination == Denomination.Four && suit == Suit.Diamonds => FourOfDiamonds,
                _ when denomination == Denomination.Five && suit == Suit.Diamonds => FiveOfDiamonds,
                _ when denomination == Denomination.Six && suit == Suit.Diamonds => SixOfDiamonds,
                _ when denomination == Denomination.Seven && suit == Suit.Diamonds => SevenOfDiamonds,
                _ when denomination == Denomination.Eight && suit == Suit.Diamonds => EightOfDiamonds,
                _ when denomination == Denomination.Nine && suit == Suit.Diamonds => NineOfDiamonds,
                _ when denomination == Denomination.Ten && suit == Suit.Diamonds => TenOfDiamonds,
                _ when denomination == Denomination.Jack && suit == Suit.Diamonds => JackOfDiamonds,
                _ when denomination == Denomination.Queen && suit == Suit.Diamonds => QueenOfDiamonds,
                _ when denomination == Denomination.King && suit == Suit.Diamonds => KingOfDiamonds,
                _ when denomination == Denomination.Ace && suit == Suit.Hearts => AceOfHearts,
                _ when denomination == Denomination.Two && suit == Suit.Hearts => TwoOfHearts,
                _ when denomination == Denomination.Three && suit == Suit.Hearts => ThreeOfHearts,
                _ when denomination == Denomination.Four && suit == Suit.Hearts => FourOfHearts,
                _ when denomination == Denomination.Five && suit == Suit.Hearts => FiveOfHearts,
                _ when denomination == Denomination.Six && suit == Suit.Hearts => SixOfHearts,
                _ when denomination == Denomination.Seven && suit == Suit.Hearts => SevenOfHearts,
                _ when denomination == Denomination.Eight && suit == Suit.Hearts => EightOfHearts,
                _ when denomination == Denomination.Nine && suit == Suit.Hearts => NineOfHearts,
                _ when denomination == Denomination.Ten && suit == Suit.Hearts => TenOfHearts,
                _ when denomination == Denomination.Jack && suit == Suit.Hearts => JackOfHearts,
                _ when denomination == Denomination.Queen && suit == Suit.Hearts => QueenOfHearts,
                _ when denomination == Denomination.King && suit == Suit.Hearts => KingOfHearts,
                _ when denomination == Denomination.Ace && suit == Suit.Spades => AceOfSpades,
                _ when denomination == Denomination.Two && suit == Suit.Spades => TwoOfSpades,
                _ when denomination == Denomination.Three && suit == Suit.Spades => ThreeOfSpades,
                _ when denomination == Denomination.Four && suit == Suit.Spades => FourOfSpades,
                _ when denomination == Denomination.Five && suit == Suit.Spades => FiveOfSpades,
                _ when denomination == Denomination.Six && suit == Suit.Spades => SixOfSpades,
                _ when denomination == Denomination.Seven && suit == Suit.Spades => SevenOfSpades,
                _ when denomination == Denomination.Eight && suit == Suit.Spades => EightOfSpades,
                _ when denomination == Denomination.Nine && suit == Suit.Spades => NineOfSpades,
                _ when denomination == Denomination.Ten && suit == Suit.Spades => TenOfSpades,
                _ when denomination == Denomination.Jack && suit == Suit.Spades => JackOfSpades,
                _ when denomination == Denomination.Queen && suit == Suit.Spades => QueenOfSpades,
                _ when denomination == Denomination.King && suit == Suit.Spades => KingOfSpades,
                _ => throw new ArgumentException($"{denomination.Name} of {suit.Name} is not a valid Card")
            };

            public static Card Parse(string? str)
            {
                ArgumentNullException.ThrowIfNull(str);

                Match match = Regex().Match(str);

                if(!match.Success) throw new ArgumentException($"{str} is not a valid Card", nameof(str));

                Denomination denomination = Denomination.Parse(match.Groups["denomination"].Value);
                Suit suit = Suit.Parse(match.Groups["suit"].Value);

                return Get(denomination, suit);
            }
        }

        public static class Hand
        {
            public static List<Card> Parse(string? str)
            {
                ArgumentNullException.ThrowIfNull(str);

                List<Card> cards = new(str
                    .Split(',')
                    .Select(Card.Parse));

                if(cards.Count == 0) throw new ArgumentException($"{str} is not a valid List<Card>", nameof(str));

                return cards;
            }

            public static List<List<Card>> GenerateCombinations(this List<Card> cards) =>
                Enumerable
                    .Range(0, 1 << cards.Count) //For each possible combination of n bits, where n is the number of cards
                    .Select(bits => cards
                        .Where((card, index) => ((1 << index) & bits) != 0) //If the bitmasked index isn't all zeros
                        .ToList())
                    .Where(combination => combination.Count >= 2) //If the size of the combination is greater than or equal to two
                    .ToList();

            public static List<List<Card>> GetFifteens(this List<List<Card>> combinations) =>
                combinations
                    .Where(combination => combination
                        .Sum(card => card.Denomination.ArithmeticValue) == 15) //If the combination sums to fifteen
                    .ToList();

            public static List<List<Card>> GetPairs(this List<List<Card>> combinations) =>
                combinations
                    .Where(combination => combination.Count == 2 && combination
                        .Skip(1)
                        .All(card => card.Denomination == combination[0].Denomination)) //If the rest of the combination is of the same denomination as the first card
                    .ToList();

            public static List<Tuple<List<Card>, int>> GenerateRuns(this List<Card> cards, List<Tuple<List<Card>, int>> runs)
            {
                int length = 1;
                int multiplier = 1;
                int count = 1;
                List<Card> run = [];

                List<Card> copy = new(cards);

                Card? previous = null;

                for(int index = 0; index < cards.Count - 1; index++) //Domain [0, n-1], where n is the number of cards
                {
                    Card first = cards[index];
                    Card second = cards[index + 1];

                    if(first.Denomination == second.Denomination) //If the denominations of the first and second cards match
                    {
                        if(previous is null || first.Denomination != previous.Denomination) //If the third card isn't the same denomination
                        {
                            multiplier *= 2; //Double the multiplier
                        }
                        else if(multiplier != 3) //If the multiplier isn't three
                        {
                            multiplier += multiplier / 2; //Add half the current multiplier
                        }
                        else //Otherwise
                        {
                            multiplier++; //Increment the multiplier
                        }

                        count++;

                        previous = first;
                    }
                    else if(first.Denomination.IsPrevious(second.Denomination)) //If the denomination of the first card is consecutive to the denomination of the second card
                    {
                        length++; //Increment the length
                        count++;
                    }
                    else if(length < 3) //If the run ends, and the length isn't greater than or equal to three
                    {
                        length = 1; //Reset the length
                        multiplier = 1; //Reset the multiplier
                        run.Clear(); //Reset the run
                    }

                    if(copy.Count > 0) //If there are more cards
                    {
                        copy.RemoveAt(0); //Remove the first card
                    }

                    if(index == count - 1) break; //If this is the end of this run, stop looping
                }

                if(length >= 3) //If a run was found
                {
                    run.AddRange(cards.GetRange(0, count)); //Add the cards to the list
                    runs.Add(Tuple.Create(run, length * multiplier)); //Add the cards and point value to the list

                    return GenerateRuns(copy, runs); //Check for more runs
                }

                return runs;
            }

            public static List<Tuple<Card, Card>> GetKnobs(this List<Card> cards, List<Card> cuts) =>
                cards
                    .Where(card => card.Denomination == Denomination.Jack && cuts.Any(cut => card.Suit == cut.Suit)) //If the card is a Jack and the suit matches any of the cuts
                    .Zip(cuts, (card, cut) => Tuple.Create(card, cut)) //Add the card and cut
                    .ToList();

            public static List<Card> GetFlush(this List<Card> cards, List<Card> cuts)
            {
                if(!cards
                    .Skip(1)
                    .All(card => card.Suit == cards[0].Suit)) return []; //If the cards don't make a flush, return an empy list

                List<Card> copy = new(cards); //Otherwise, make a copy of the cards

                copy.AddRange(cuts
                    .Where(cut => cut.Suit == cards[0].Suit)); //Add any cuts that match the suit

                return copy;
            }
        }
    }
}
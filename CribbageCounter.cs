using Cards;
using System.Text.RegularExpressions;

Console.WriteLine("Enter your hand, then press Enter");

HandParser parser = new();

parser.Parse(Console.ReadLine());

Console.WriteLine("Enter the cut card(s), then press Enter");

parser.Parse(Console.ReadLine());

CardCollection? hand = parser.Hand;
CardCollection? cutCards = parser.Cuts;

if(hand is not null && cutCards is not null)
{
    HandCounter counter = new HandCounter(hand, cutCards);

    int fifteens = counter.GetFifteens();
    int pairs = counter.GetPairs();
    int runs = counter.GetRuns();
    int knobs = counter.GetKnobs();
    int flush = counter.GetFlush();
    int points = fifteens + pairs + runs + knobs + flush;

    Console.WriteLine("");

    Console.WriteLine("Fifteens: " + fifteens);
    Console.WriteLine("Pairs: " + pairs);
    Console.WriteLine("Runs: " + runs);
    Console.WriteLine("Knobs: " + knobs);
    Console.WriteLine("Flush: " + flush);
    Console.WriteLine("Points: " + points);

    Console.WriteLine("");

    Console.Write("Press any key to continue...");
    Console.ReadKey(true);
}

namespace Cards
{
    public class Denomination : IComparable<Denomination>
    {
        public string Name { get; init; }
        public int ArithmeticValue { get; init; }
        public int SequentialValue { get; init; }
        
        public Denomination(string name, int value)
        {
            this.Name = name;
            this.ArithmeticValue = Math.Min(value, 10);
            this.SequentialValue = value;
        }

        public static int operator +(Denomination left, Denomination right) => left.ArithmeticValue + right.ArithmeticValue;

        public static int operator -(Denomination left, Denomination right) => left.ArithmeticValue - right.ArithmeticValue;

        public static bool operator ==(Denomination left, Denomination right) => left.Equals(right);

        public static bool operator !=(Denomination left, Denomination right) => !(left == right);

        public static bool operator <(Denomination left, Denomination right) => left.SequentialValue < right.SequentialValue;

        public static bool operator >(Denomination left, Denomination right) => left.SequentialValue > right.SequentialValue;

        public static bool operator <=(Denomination left, Denomination right) => left.SequentialValue <= right.SequentialValue;

        public static bool operator >=(Denomination left, Denomination right) => left.SequentialValue >= right.SequentialValue;

        public bool Precedes(Denomination denomination) => denomination - this == 1;

        public bool Follows(Denomination denomination) => this - denomination == 1;

        public int CompareTo(Denomination? other) => other is null ? 1 : SequentialValue.CompareTo(other.SequentialValue);

        public override bool Equals(object? obj) =>
            (obj is Denomination denomination && SequentialValue.Equals(denomination.SequentialValue)) ||
            (obj is string name && Name.Equals(name)) ||
            (obj is int value && SequentialValue.Equals(value));

        public override int GetHashCode() => (Name, ArithmeticValue, SequentialValue).GetHashCode();

        public static Denomination Parse(string name) => name.ToUpper() switch
        {
            "A" => Denominations.ACE,
            "2" => Denominations.TWO,
            "3" => Denominations.THREE,
            "4" => Denominations.FOUR,
            "5" => Denominations.FIVE,
            "6" => Denominations.SIX,
            "7" => Denominations.SEVEN,
            "8" => Denominations.EIGHT,
            "9" => Denominations.NINE,
            "10" => Denominations.TEN,
            "J" => Denominations.JACK,
            "Q" => Denominations.QUEEN,
            "K" => Denominations.KING,
            _ => throw new ArgumentException(null, nameof(name))
        };
    }

    public static class Denominations
    {
        public static readonly Denomination ACE = new("A", 1);
        public static readonly Denomination TWO = new("2", 2);
        public static readonly Denomination THREE = new("3", 3);
        public static readonly Denomination FOUR = new("4", 4);
        public static readonly Denomination FIVE = new("5", 5);
        public static readonly Denomination SIX = new("6", 6);
        public static readonly Denomination SEVEN = new("7", 7);
        public static readonly Denomination EIGHT = new("8", 8);
        public static readonly Denomination NINE = new("9", 9);
        public static readonly Denomination TEN = new("10", 10);
        public static readonly Denomination JACK = new("J", 11);
        public static readonly Denomination QUEEN = new("Q", 12);
        public static readonly Denomination KING = new("K", 13);
    }

    public class Suit : IComparable<Suit>
    {
        public string Name { get; init; }

        public Suit(string name)
        {
            this.Name = name;
        }

        public static bool operator ==(Suit left, Suit right) => left.Equals(right);

        public static bool operator !=(Suit left, Suit right) => !(left == right);

        public int CompareTo(Suit? other) => other is null ? 1 : Name.CompareTo(other.Name);

        public override bool Equals(object? obj) =>
            (obj is Suit suit && Name.Equals(suit.Name)) ||
            (obj is string name && Name.Equals(name));

        public override int GetHashCode() => Name.GetHashCode();

        public static Suit Parse(string name) => name.ToUpper() switch
        {
            "C" => Suits.CLUBS,
            "D" => Suits.DIAMONDS,
            "H" => Suits.HEARTS,
            "S" => Suits.SPADES,
            _ => throw new ArgumentException(null, nameof(name))
        };
    }

    public static class Suits
    {
        public static readonly Suit CLUBS = new("C");
        public static readonly Suit DIAMONDS = new("D");
        public static readonly Suit HEARTS = new("H");
        public static readonly Suit SPADES = new("S");
    }

    public class Card : IComparable<Card>
    {
        public Denomination Denomination { get; init; }
        public Suit Suit { get; init; }
        public string Name => Denomination.Name + Suit.Name;

        public Card(Denomination denomination, Suit suit)
        {
            this.Denomination = denomination;
            this.Suit = suit;
        }

        public static bool operator ==(Card left, Card right) => left.Equals(right);

        public static bool operator !=(Card left, Card right) => !(left == right);

        public int CompareTo(Card? other) => other is null ? 1 : (Denomination, Suit).CompareTo((other.Denomination, other.Suit));

        public override bool Equals(object? obj) => obj is Card card && (Denomination, Suit).Equals((card.Denomination, card.Suit));

        public override int GetHashCode() => (Denomination, Suit).GetHashCode();

        public static Card Parse(string name)
        {
            string denomination = Regex.Match(name, "([AJQK]|[2-9]|10)", RegexOptions.IgnoreCase).Value;
            string suit = Regex.Match(name, "([CDHS])", RegexOptions.IgnoreCase).Value;

            if(denomination is null || suit is null) throw new ArgumentException(null, nameof(name));

            return new Card(Denomination.Parse(denomination), Suit.Parse(suit));
        }
    }

    public class CardCollection : ICloneable
    {
        public List<Card> Cards { get; init; }
        public Card FirstCard => Cards[0];
        public int Count => Cards.Count;

        public CardCollection(params Card[] cards)
        {
            this.Cards = new(cards);
        }

        public CardCollection(List<Card> cards)
        {
            this.Cards = cards;
        }

        public Card this[int index]
        {
            get => Cards[index];
            set => Cards[index] = value;
        }

        public void Remove(Card card) => Cards.Remove(card);

        public void Remove(int index) => Cards.RemoveAt(index);

        public void Insert(int index, Card card) => Cards.Insert(index, card);

        public void Add(Card card) => Cards.Add(card);

        public void Merge(CardCollection hand) => Cards.AddRange(hand.Cards);

        public void Sort() => Cards.Sort();

        public object Clone() => new CardCollection { Cards = new List<Card>(Cards) }; //Deep clone the hand

        public static CardCollection Merge(CardCollection hand, CardCollection cuts)
        {
            if(hand.Clone() is not CardCollection clone) throw new ArgumentNullException(nameof(hand));

            clone.Merge(cuts);

            return clone;
        }
    }

    public class Relationship
    {
        public List<Card> Cards { get; init; }
        public Card FirstCard => Cards[0];
        public int Count => Cards.Count;
        public bool IsFifteen => Cards.Sum(card => card.Denomination.ArithmeticValue) == 15; //If the sum of all the cards' denominations is 15
        public bool IsPair => Count == 2 && Cards //If the number of cards in the relationship is 2
            .Skip(1)
            .All(card => card.Denomination == FirstCard.Denomination); //And if the first and second cards' denominations match

        public Relationship(params Card[] cards)
        {
            this.Cards = new(cards);
        }

        private static void CreateRelationships(List<Relationship> relationships, CardCollection hand, int count, List<Card> cards, int start, int position, int size)
        {
            if(position == size) //If the current relationship has reached the designated size
            {
                relationships.Add(new(cards.GetRange(0, size).ToArray())); //Add the relationship to the list
            }

            for(int index = start; index < count; index++) //Domain [a, b] where a is the index of the first card of the relationship and b is the number of cards in the hand
            {
                cards.Insert(position, hand[index]); //Insert the card found at index into the card cache at position

                Relationship.CreateRelationships(relationships, hand, count, cards, index + 1, position + 1, size); //Create all possible relationships with the inserted card

                cards.RemoveAt(cards.Count - 1); //Then remove the card from the cache
            }
        }

        public static List<Relationship> CreateRelationships(CardCollection hand)
        {
            List<Relationship> relationships = new();
            int count = hand.Count;

            for(int index = 2; index <= count; index++) //Domain [2, b] where b is the number of cards in the hand
            {
                Relationship.CreateRelationships(relationships, hand, count, new(), 0, 0, index); //Create all possible relationships of size index
            }

            return relationships;
        }
    }

    public class HandParser
    {
        public CardCollection? Hand { get; set; }
        public CardCollection? Cuts { get; set; }

        public void Parse(string? hand)
        {
            if(hand is null) throw new ArgumentNullException(nameof(hand));

            List<Card> cards = new(hand
                .Split(' ')
                .Select(name => Card.Parse(name)));

            if(Hand == null)
            {
                Hand = new(cards);
            }
            else Cuts ??= new(cards);
        }
    }

    public class HandCounter
    {
        public CardCollection Hand { get; init; }
        public CardCollection Cuts { get; init; }
        public CardCollection MergedHand { get; init; }
        public List<Relationship> Relationships { get; init; }

        public HandCounter(CardCollection hand, CardCollection cuts)
        {
            this.Hand = hand;
            this.Cuts = cuts;
            this.MergedHand = CardCollection.Merge(hand, cuts);

            Hand.Sort();
            Cuts.Sort();
            MergedHand.Sort();

            this.Relationships = Relationship.CreateRelationships(MergedHand);
        }

        public int GetFifteens()
        {
            return Relationships.Sum(relationship => relationship.IsFifteen ? 2 : 0); //y = 2x where x is the number of relationships that sum to 15
        }

        public int GetPairs()
        {
            return Relationships
                .Where(relationship => relationship.Count == 2) //Filter only relationships that have 2 cards
                .Sum(relationship => relationship.IsPair ? 2 : 0); //y = 2x where x is the number of relationships that are pairs
        }

        private int GetRuns(CardCollection hand)
        {
            int length = 1; //Length of the run
            int multiplier = 1; //The number of unique combinations the run makes

            int count = hand.Count;

            if(MergedHand.Clone() is not CardCollection clone) throw new InvalidCastException();

            int offset = 0;

            Card? previous = null;

            for(int index = 0; index < count - 1; index++) //Domain [0, x-1] where x is the number of cards in the hand
            {
                Card current = hand[index];
                Card next = hand[index + 1];

                if(current.Denomination == next.Denomination) //If the first and second cards' denominations match
                {
                    if (previous is not null && current.Denomination == previous.Denomination) //And if the previous card exists and is the same denomination
                    {
                        multiplier += multiplier == 3 ? 1 : (multiplier / 2); //And if there are 3 unique combinations already, add 1; otherwise, add half the number of unique combinations
                    }
                    else
                    {
                        multiplier *= 2; //Otherwise, double the multiplier
                    }

                    previous = next;
                }
                else if(current.Denomination.Precedes(next.Denomination)) //If the first card comes exactly one before the second card, i.e. 2, 3
                {
                    length++;
                }
                else if(length < 3) //If the run ends and isn't 3 or greater in length
                {
                    length = 1;
                    multiplier = 1;
                }
                else break; //Otherwise, stop counting

                if(index - offset < clone.Count) //If there are more cards in the hand after this run
                {
                    clone.Remove(index - offset); //Remove the cards from this run

                    offset++;
                }
            }

            if(length >= 3) //If the run exists
            {
                return (length * multiplier) + this.GetRuns(clone); //y1 = (x1 * z1) + y2 + y3 + ... + yn where x1 is the length of the run, z1 is the number of unique combinations the run makes, and yn is the point value of any subsequent runs
            }

            return 0;
        }

        public int GetRuns()
        {
            return this.GetRuns(MergedHand);
        }

        public int GetKnobs()
        {
            return Hand.Cards
                .Where(card => card.Denomination == Denominations.JACK) //Filter only cards in the hand that are Jacks
                .Sum(card => Cuts.Cards.Count(cutCard => card.Suit == cutCard.Suit)); //y = x where x is the number of cut cards that match the suit of any Jacks in the hand
        }

        public int GetFlush()
        {
            return Hand.Cards
                .Skip(1)
                .All(card => card.Suit == Hand.FirstCard.Suit) ? Hand.Count + Cuts.Cards.Count(card => card.Suit == Hand.FirstCard.Suit) : 0; //y = x + z; Domain [x, x+z] where x and z are the number of cards that match the suit of the first card from the hand and cut cards, respectively
        }
    }
}
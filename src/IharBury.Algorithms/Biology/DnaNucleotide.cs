using System;

namespace IharBury.Algorithms.Biology
{
    public enum DnaNucleotide
    {
        Adenine = 0,
        Cytosine = 1,
        Guanine = 2,
        Thymine = 3
    }

    public static class DnaNucleotideExtensions
    {
        public static char ToSymbol(this DnaNucleotide nucleotide)
        {
            switch (nucleotide)
            {
                case DnaNucleotide.Adenine:
                    return 'A';
                case DnaNucleotide.Cytosine:
                    return 'C';
                case DnaNucleotide.Guanine:
                    return 'G';
                case DnaNucleotide.Thymine:
                    return 'T';
                default:
                    throw new ArgumentOutOfRangeException(nameof(nucleotide));
            }
        }

        public static DnaNucleotide ParseDnaNucleotideSymbol(this char symbol)
        {
            switch (symbol)
            {
                case 'A':
                case 'a':
                    return DnaNucleotide.Adenine;
                case 'C':
                case 'c':
                    return DnaNucleotide.Cytosine;
                case 'G':
                case 'g':
                    return DnaNucleotide.Guanine;
                case 'T':
                case 't':
                    return DnaNucleotide.Thymine;
                default:
                    throw new ArgumentOutOfRangeException(nameof(symbol));
            }
        }
    }
}

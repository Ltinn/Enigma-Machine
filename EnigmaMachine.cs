using System;
using System.Collections.Generic;

namespace EnigmaMachine {
    /// <summary>
    /// Enigma Machine created by Ltin
    /// Licensed under the MIT license;
    /// 
    /// This is an enigma machine. It should not be used for legitimate attempts at encryption. Each new instance of the enigma machine has a random set of rotors, so your results will vary each time you restart your program, and messages encrypted with different rotors will not be able to be decrypted. 
    /// 
    /// Let me re-iterate: DO NOT USE THIS TO ATTEMPT TO ENCRYPT ACTUALLY IMPORTANT INFORMATION! IT IS NOT SECURE! THIS IS FOR FUN!
    /// 
    /// To use: 
    /// first create an instance of the enigma machine. 
    /// Then call Scramble(int, int, int, string) where the first three ints are your rotor positions (1 - 26 inclusive) and the string is what you want to encrypt.
    /// To decrypt, use the race rotor positions that were used when the message was encrypted and put the encrypted message in as your string.
    /// </summary>
    public class EnigmaMachine {
        private Rotor firstRotor = new Rotor();
        private Rotor secondRotor = new Rotor();
        private Rotor thirdRotor = new Rotor();
        static Random rand = new Random();

        //default constructor
        public EnigmaMachine(int seed){
            rand = new Random(seed);
        }

        /// <summary>
        /// This method takes the input and scrambles it with the Engima Machine. 
        /// The rotors must be integers between 1 and 26 inclusive. It throws an exception if the rotor positions are bad.
        /// </summary>
        /// <param name="rotor1">The integer position of the first rotor.</param>
        /// <param name="rotor2">The integer position of the second rotor.</param>
        /// <param name="rotor3">The integer position of the third rotor.</param>
        /// <param name="input">The string being scrmabled.</param>
        /// <returns>The scrambled (or unscrambled) string. Only characters are scrambled, and the output will always be upper case.</returns>
        public String Scramble(int rotor1, int rotor2, int rotor3, string input) {
            if (rotor1 > 26 || rotor2 > 26 || rotor3 > 26 || rotor1 < 1 || rotor2 < 1 || rotor3 < 1) {
                throw new Exception("Rotor position impossible. Rotors must be between 1 and 26 inclusive.");
            }

            //make string all caps
            input = input.ToUpper();

            String output = "";

            //scramble for each character in the string
            foreach(char ch in input) {
                char c = ch; //this is the character we will work with
                if (IsUpperCaseLetter(c)) {
                    c = firstRotor.RotorScramble1(c, rotor1);
                    c = secondRotor.RotorScramble1(c, rotor2);
                    c = thirdRotor.RotorScramble1(c, rotor3);
                    //reflector
                    c = Reflector(c);
                    c = thirdRotor.RotorScramble2(c, rotor3);
                    c = secondRotor.RotorScramble2(c, rotor2);
                    c = firstRotor.RotorScramble2(c, rotor1);

                    //now rotors increment
                    rotor3++;
                    if (rotor3 > 26) {
                        rotor3 = 1;
                        rotor2++;
                        if (rotor2 > 26) {
                            rotor2 = 1;
                            rotor1++;
                            if (rotor1 > 26) {
                                rotor1 = 1;
                            }
                        }
                    }
                }

                output += c; //this goes here to ensure all non-letter characters are preserved in the message
            }

            return output;
        }

        private char Reflector(char c) {
            if (c == 'Q') {
                return 'M';
            } else if (c == 'W') {
                return 'N';
            } else if (c == 'E') {
                return 'B';
            } else if (c == 'R') {
                return 'V';
            } else if (c == 'T') {
                return 'C';
            } else if (c == 'Y') {
                return 'X';
            } else if (c == 'U') {
                return 'Z';
            } else if (c == 'I') {
                return 'L';
            } else if (c == 'O') {
                return 'K';
            } else if (c == 'P') {
                return 'J';
            } else if (c == 'A') {
                return 'H';
            } else if (c == 'S') {
                return 'G';
            } else if (c == 'D') {
                return 'F';
            } else if (c == 'F') {
                return 'D';
            } else if (c == 'G') {
                return 'S';
            } else if (c == 'H') {
                return 'A';
            } else if (c == 'J') {
                return 'P';
            } else if (c == 'K') {
                return 'O';
            } else if (c == 'L') {
                return 'I';
            } else if (c == 'Z') {
                return 'U';
            } else if (c == 'X') {
                return 'Y';
            } else if (c == 'C') {
                return 'T';
            } else if (c == 'V') {
                return 'R';
            } else if (c == 'B') {
                return 'E';
            } else if (c == 'N') {
                return 'W';
            } else //C=='M')
             {
                return 'Q';
            }
        }

        private Boolean IsUpperCaseLetter(char c) {
            return c == 'Q' || c == 'W' || c == 'E' || c == 'R' || c == 'T' || c == 'Y' || c == 'U' || c == 'I' || c == 'O' || c == 'P' || c == 'A' || c == 'S' ||
                c == 'D' || c == 'F' || c == 'G' || c == 'H' || c == 'J' || c == 'K' || c == 'L' || c == 'Z' || c == 'X' || c == 'C' || c == 'V' || c == 'B' ||
                c == 'N' || c == 'M'; //this is qwerty order; It was the easiest way to type it quickly.
        }

        protected class Rotor {
            private List<char> chars = new List<char>();
            private List<char> rotor = new List<char>();
            //will be the same as above, but in reverse order
            private List<char> rotorReverse = new List<char>();

            public Rotor() {
                //populate list w/ the alphabet
                for (int i = 'A'; i <= 'Z'; i++) {
                    chars.Add((char) i);

                    //randomly order the rotor by creating a list
                    while (chars.Count > 0) {
                        int randomIndex = rand.Next(chars.Count);
                        char charToAdd = chars[randomIndex];
                        chars.RemoveAt(randomIndex);
                        rotor.Add(charToAdd);
                        rotorReverse.Insert(0, charToAdd);
                    }
                }
            }


            /// <summary>
            /// Scrambles a character.
            /// It does not matter whether you call rotorScramble1 or rotorScramble2 first, but the other one must be called the second time the rotor is used for each character, otherwise your output will be the same as your input.
            /// </summary>
            /// <param name="c">The character being scrambled.</param>
            /// <param name="position">The position of the rotor</param>
            /// <returns>the scrambled character</returns>
            public char RotorScramble1(char c, int position) {
                int charIndex = rotor.IndexOf(c);
                int outputIndex = charIndex + position;
                if (outputIndex >= 26) {
                    outputIndex = outputIndex - 26;
                }
                return rotor[outputIndex];
            }

            /// <summary>
            /// Scrambles a character.
            /// It does not matter whether you call rotorScramble1 or rotorScramble2 first, but the other one must be called the second time the rotor is used for each character, otherwise your output will be the same as your input.
            /// </summary>
            /// <param name="c">The character being scrambled.</param>
            /// <param name="position">The position of the rotor</param>
            /// <returns>the scrambled character</returns>
            public char RotorScramble2(char c, int position) {
                int charIndex = rotorReverse.IndexOf(c);
                int outputIndex = charIndex + position;
                if (outputIndex >= 26) {
                    outputIndex = outputIndex - 26;
                }
                return rotorReverse[outputIndex];
            }
        }
    }
}

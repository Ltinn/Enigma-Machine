# Enigma-Machine
A class which simulates an enigma machine.

To use, first create a new EnigmaMachine object. If no seed is provided, then each new one will contain different scramblers, which are generated randomly, so a message encrypted in one object wont be able to be decrypted in a different object.
If a seed is supplied then the same object will be created every time.

Then to encrypt or decrypt a message, call the Scramble() method with four parameters: three integers, and a string which is whatever you intend to encrypt. The three integers should be between 1 and 26 inclusive. They are your "rotor positions."
The method returns a string which is your encrypted message. The message will always be upper case, and only the 26 letters of the English alphabet will be encrypted. Everything else, including numbers, symbols, and spaces, will be left as is.
To decrypt, send the encrypted message through the same enigma machine with the same rotor settings. The method will return the decrypted message.

It should go without saying that this isnt going to work for anything you need legitimatly encrypted. As far as I can tell, it has the same flaws that the orignial German machines had, which made them easily breakable back then. Obviously you shouldn't use it for anything that actually needs to be kept secure.
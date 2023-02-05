import random

print("Welcome to the guessing game!")
print("Guess a number between 1 and 100")

random_number = random.randint(1, 100)

guess = None
while guess != random_number:
	guess = int(input("Your guess: "))

	if guess > random_number:
		print("Too high, try again")
	elif guess < random_number:
		print("Too low, try again")
	else:
		print("Correct! You win!")
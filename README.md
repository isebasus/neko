# DABABY

This directory contains the the files needed to show that I have created my git environment in my local VM and have read the cheating agreement. *To see what each file does, visit Files.* 

## What you need

To clone this repository you'll need Git and for the hello.c file to compile you'll need clang. 

### Prerequisites

To install git on your machine run:

```
$ sudo apt-get install git
```

and to install clang run:

```
$ sudo apt-get install clang
```

### Running

To clone the repository it is you should preferrably use SSH over HTTPS, but since your running the files in the repo, just use HTTPS.

To clone run:

```
$ git clone https://git.ucsc.edu/cse13s/spring2021/sicarbon.git
```

then cd into the asgn0 directory

```
$ cd sicarbon/asgn0
```

then compile the program by running:

```
$ clang -Wall -Wextra -Werror -Wpendantic -o hello hello.c
```

and to view the output run:

```
./hello
```

## Files

See what each file does

### CHEATING.pdf

This file contains the student agreement to acknowledge the Academic Honesty Policy.

### hello.c

This file contains the C code that prints out "Hello World!" after compiled. 

## Authors

* **Sebastain Carbonero**



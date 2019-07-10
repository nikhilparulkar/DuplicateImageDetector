# DuplicateImageDetector
This is a console application written in C# on .Net Core 2.1 Framework.

Objective:
Objective of this console application is to detect the multiple copies of images in folder. The result of this application prints 2 Lists on screen. 
One for Original copies (provide complete path of images)
Second list for Duplicates (provide complete path of duplicate images)

Installation and execution :
1) Require .net core 2.1 framework.
2) Open MS solution file(.sln) and hit debug> start debugging
3) On Excution this application opens a console window and ask for complete path of folder where duplicate images are to be detected.


Design:
Application has been written with focus on decoupled code, Unit tests and S.O.L.I.D design principle. Additionally Logging frawork NLog 
has been sued for logging.

Algorithm: 
The basis of current application in that every file will have distinct MD5 Hash value. If two file are same then they will same Md5hash Values, otherwise not.
For each file in target directory a MD5Hash is calculated and store in Dictionary (Map) with key as MD5Hash value. If subsequent file with same MD5Hash is found 
then that file is kept in duplicate list.

Search complexity in O(logN).


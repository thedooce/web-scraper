# web-scraper

---DESCRIPTION---
This project is for XCentium's coding challenge.

This utility is a single web-page that allows the user to enter a web url. The tool then navigates to that page, retrieves the word count, 
and retrieves images to put into a carousel.

The 7 most frequently found words are put into a table along with the total word count of that page.
The images are put into a carousel, complete with controls.

---HOW TO RUN--
This project is meant to be run from within Visual Studio. To run, simply open the .sln file, and start debugging (usually F5).
This will open a browser window in which the page is opened.

---HOW TO USE---
With the project running, enter a URL from a webpage in the text box, and then press the "Scan" button. 
Note: The URL needs to include the protocol. It's safest to copy and paste the url from the address bar of a page you're visiting.
Example: http://www.msnbc.com/

---ABOUT---
This project was created in Visual Studio Community 2017 on a Windows 10 PC. 
I created it using an ASP.NET Web Application (Web Forms) project template.
The only external tools used were Bootstrap (included in the project) for displaying the carousel, and 
HTML Agility Pack (nuget package) for parsing a web page.
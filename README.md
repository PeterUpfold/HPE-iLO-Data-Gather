HPE iLO Data Gather
===================

Gather data (currently just fan speed information) by screen-scraping the HPE iLO interface.

This is used to gather operational data on an HPE DL385 G7 server running FreeBSD.

This very hacky solution works around the fact that there is no System Management Homepage or similar interface
for FreeBSD. We do, however, have access to valuable data through the iLO system. Unfortunately it seems
that SNMP support in iLO Standard is limited to SNMP traps and data like fan speed cannot be queried over SNMP.

So, this project uses Selenium WebDriver to launch a web browser, log in to iLO (with a minimally privileged account), click
the appropriate buttons and extract the information of interest.

## Usage

Download an appropriate [`ChromeDriver.exe`](https://sites.google.com/a/chromium.org/chromedriver/downloads) and [`GeckoDriver.exe`](https://github.com/mozilla/geckodriver/releases) and place in the project folder.

Build the project. In the `bin\Debug` or `bin\Release` folder, edit `HPE-iLO-Data-Gather.exe.config`, edit `iLOURL`,
`iLOUsername` and `iLOPassword` as appropriate. **Always use a minimally privileged account**, as the password must
be stored in this `.config` file. Protect the `.config` file using file permissions, disk encryption, etc.

Tested currently with only HPE iLO 3 1.91 on a DL385 G7.

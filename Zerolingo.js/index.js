const puppeteer = require('puppeteer');


let navigateToSite = async () => {
    const browser = await puppeteer.launch({
        headless: false,
        defaultViewport: null,
        timeout: 10000
    });

    const version = await browser.version();
    console.log(version);

    const page = await browser.newPage();

    await page.goto('https://duolingo.com');
}

navigateToSite();
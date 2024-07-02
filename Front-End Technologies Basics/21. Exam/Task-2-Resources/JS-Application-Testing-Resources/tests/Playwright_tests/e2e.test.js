const { test, describe, beforeEach, afterEach, beforeAll, afterAll, expect } = require('@playwright/test');
const { chromium } = require('playwright');
 
const host = 'http://localhost:3000'; // Application host (NOT service host - that can be anything)
 
let browser;
let context;
let page;
 
let user = {
    email : "",
    password : "123456",
    confirmPass : "123456",
};
 
let albumName = "";
 
describe("e2e tests", () => {
    beforeAll(async () => {
        browser = await chromium.launch();
    });
 
    afterAll(async () => {
        await browser.close();
    });
 
    beforeEach(async () => {
        context = await browser.newContext();
        page = await context.newPage();
    });
 
    afterEach(async () => {
        await page.close();
        await context.close();
    });
 
    
    describe("authentication", () => {
        test("User is able to register with valid data", async()=>{
            //arrange
            await page.goto(host);
            await page.click("text=Register");
            await page.waitForSelector('form');
 
            let random = Math.floor(Math.random() * 10000);
            user.email = `email${random}@abv.bg`;
            //act
            await page.locator('#email').fill(user.email);
            await page.locator('#password').fill(user.password);
            await page.locator('#conf-pass').fill(user.confirmPass);
 
            let [response] = await Promise.all([page.waitForResponse(response => response.url().includes("/users/register" )
             && response.status() == 200), page.click('[type ="submit"]')
            ]);     //9.    Get the response by creating a Promise scope
            let userData=await response.json();       //11. Parse the response to JSON:
 
            //assert
            await expect(response.ok()).toBeTruthy();       //10.   Assert that the response is okey.
            expect(userData.email).toBe(user.email);        //12.   Assert that the email and password are as expected.
            expect(userData.password).toBe(user.password);
        })
        test("User is able to logout from the app", async()=>{
            //arrange
            await page.goto(host);
            await page.click("text=Login");
            await page.waitForSelector('form');
            await page.locator('#email').fill(user.email);
            await page.locator('#password').fill(user.password);
            await page.click('[type="submit"]');
            //act
            let [response] = await Promise.all([page.waitForResponse(response => response.url().includes("/users/logout" )
            && response.status() == 204), page.click("text=Logout")
           ]);     //9. Get the response by creating a Promise scope
 
           //assert
           expect(response.ok()).toBeTruthy();       //10.  Assert that the response is okey.
           await page.waitForSelector('text=Login');
           expect(page.url()).toBe(host + '/');
        })
    });
 
    describe("navbar", () => {
        test("Navigation for logged-In user is correct", async()=>{
            //arrange
            await page.goto(host);
            await page.click("text=Login");
            await page.waitForSelector('form');
            //act
            await page.locator('#email').fill(user.email);
            await page.locator('#password').fill(user.password);
            await page.click('[type="submit"]');
            //assert
            await expect(page.locator('nav >> text=Home')).toBeVisible();
            await expect(page.locator('nav >> text=Catalog')).toBeVisible();
            await expect(page.locator('nav >> text=Search')).toBeVisible();
            await expect(page.locator('nav >> text=Logout')).toBeVisible();
 
            await expect(page.locator('nav >> text=Login')).toBeHidden();
            await expect(page.locator('nav >> text=Register')).toBeHidden();
        })
        test("Navigation for guest user is correct", async()=>{
            //arrange
            await page.goto(host);
            //assert
            await expect(page.locator('nav >> text=Home')).toBeVisible();
            await expect(page.locator('nav >> text=Catalog')).toBeVisible();
            await expect(page.locator('nav >> text=Search')).toBeVisible();
            await expect(page.locator('nav >> text=Login')).toBeVisible();
 
            await expect(page.locator('nav >> text=Create Album')).toBeHidden();
            await expect(page.locator('nav >> text=Logout')).toBeHidden();
        })
        
    });
 
    describe("CRUD", () => {
        test("User is able to Create an album", async ()=>{
            //arrange
            await page.goto(host);
            await page.click("text=Login");
            await page.waitForSelector('form');
            
            await page.locator('#email').fill(user.email);
            await page.locator('#password').fill(user.password);
            await page.click('[type="submit"]');
            //act   
            await page.click("text = Create Album");
            await page.waitForSelector('form');
            let random = Math.floor(Math.random() * 10000);
            albumName = `${random}_album`;
 
            await page.locator('#name').fill(albumName);
            await page.locator('#imgUrl').fill("/images/Metallica.jpg");
            await page.locator('#price').fill("20");
            await page.locator('#releaseDate').fill("29,06,2024");
            await page.locator('#artist').fill("Metallica");
            await page.locator('#genre').fill("Trash metal");
            await page.fill('[name=description]', "Master of puppets");                                               //.locator('#description').fill("Master of puppets");
            let [response] = await Promise.all([page.waitForResponse(response => response.url().includes("/data/albums")
            && response.status() == 200), page.click('[type="Submit"]')
           ]);     //9. Get the response by creating a Promise scope
           let eventData = await response.json();       //11.   Parse the response to JSON:
 
           //assert
           await expect(response.ok()).toBeTruthy();       //10.    Assert that the response is okey.
           expect(eventData.name).toBe(albumName);        //12. Assert that the email and password are as expected.
           expect(eventData.imgUrl).toBe("/images/Metallica.jpg");
           expect(eventData.price).toBe("20");
           expect(eventData.releaseDate).toBe("29,06,2024");
           expect(eventData.artist).toBe("Metallica");
           expect(eventData.genre).toBe("Trash metal");
           expect(eventData.description).toBe("Master of puppets");
        })
        test("User is able to edit an album", async ()=>{
            //arrange
            await page.goto(host);
            await page.click("text=Login");
            await page.waitForSelector('form');
            await page.locator('#email').fill(user.email);
            await page.locator('#password').fill(user.password);
            await page.click('[type="submit"]');
            //act
            await page.click("text = Search")
            await page.locator('#search-input').fill(albumName);
            await page.locator('.button-list').click();
            await page.locator('#details').first().click();
            await page.click('text = Edit');
            await page.waitForSelector('form');
            await page.locator('#price').fill("15");
 
            let [response] = await Promise.all([page.waitForResponse(response => response.url().includes("/data/albums")
            && response.status() == 200), page.click('[type="Submit"]')
           ]);     //9. Get the response by creating a Promise scope
           let eventData = await response.json();       //11.   Parse the response to JSON:
           //assert
           await expect(response.ok()).toBeTruthy();       //10.    Assert that the response is okey.
           expect(eventData.name).toBe(albumName);        //12. Assert that the email and password are as expected.
           expect(eventData.imgUrl).toBe("/images/Metallica.jpg");
           expect(eventData.price).toBe("15");
           expect(eventData.releaseDate).toBe("29,06,2024");
           expect(eventData.artist).toBe("Metallica");
           expect(eventData.genre).toBe("Trash metal");
           expect(eventData.description).toBe("Master of puppets");
        })
        test("User is able to delete an album", async ()=>{
            //arrange
            await page.goto(host);
            await page.click("text=Login");
            await page.waitForSelector('form');
            await page.locator('#email').fill(user.email);
            await page.locator('#password').fill(user.password);
            await page.click('[type="submit"]');
            //act
            await page.click("text = Search")
            await page.locator('#search-input').fill(albumName);
            await page.locator('.button-list').click();
            await page.locator('#details').first().click();
          //  await page.locator("text = Delete").click();
            let [response] = await Promise.all([page.waitForResponse(response => response.url().includes("/data/albums")
            && response.status() == 200),
            page.on('dialog', dialog => dialog.accept()),
            page.click('text = Delete')
           ]);     //9. Get the response by creating a Promise scope
 
           //assert
           await expect(response.ok()).toBeTruthy();       //10.    Assert that the response is okey.
        })
        
    });
});
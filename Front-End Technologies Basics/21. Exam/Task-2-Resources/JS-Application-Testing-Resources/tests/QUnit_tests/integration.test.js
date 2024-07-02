const baseUrl = "http://localhost:3030/";
 
let user = {
    email: "",
    password: "123456"
};
let lastCreatedAlbum = '';
let myAlbum = {
    "name": "",
    "artist": "Unknown",
    "description": "",
    "genre": "Random genre",
    "imgUrl": "/images/pinkFloyd.jpg",
    "price": "15.25",
    "releaseDate": "29 June 2024", 
}
 
let token = "";
let userId = "";
 
QUnit.config.reorder = false;
 
QUnit.module("user functionalities", () => {
    QUnit.test ("registration", async (assert) =>{
    let path = 'users/register';
 
    let random = Math.floor(Math.random() * 10000);
    let email = `abv${random}@abv.bg`;
    user.email = email;
 
    let response = await fetch(baseUrl + path, {
        method: "POST",
        headers: {
            'content-type': 'application/json'
        },
        body: JSON.stringify(user)
    });
    let json = await response.json();
 
 
    assert.ok(response.ok);
 
        assert.ok(json.hasOwnProperty('email'), "email exists");
        assert.equal(json['email'], user.email, 'expexted email');
        assert.strictEqual(typeof json.email, 'string', "email has corect type");
 
        assert.ok(json.hasOwnProperty('password'), "password exists");
        assert.equal(json['password'], user.password, 'expexted password');
        assert.strictEqual(typeof json.password, 'string', "password has corect type");
 
        assert.ok(json.hasOwnProperty('_createdOn'), "_createdOn exists");
        assert.strictEqual(typeof json._createdOn, 'number', "password has corect type");
 
        assert.ok(json.hasOwnProperty('_id'), "_id exists");
        assert.strictEqual(typeof json._id, 'string', "_id has corect type");
 
        assert.ok(json.hasOwnProperty('accessToken'), "accessToken exists");
        assert.strictEqual(typeof json.accessToken, 'string', "accessToken has corect type");
 
        token = json['accessToken'];
        userId = json['_id'];
        sessionStorage.setItem('event-user', JSON.stringify(user));
 
    });
    QUnit.test("login", async (assert) =>{
        let path = 'users/login';
 
        let response = await fetch(baseUrl + path, {
            method: "POST",
            headers: {
                'content-type': 'application/json'
            },
            body: JSON.stringify(user)
        });
        let json = await response.json();
 
        assert.ok(response.ok);
 
        assert.ok(json.hasOwnProperty('email'), "email exists");
        assert.equal(json['email'], user.email, 'expexted email');
        assert.strictEqual(typeof json.email, 'string', "email has corect type");
 
        assert.ok(json.hasOwnProperty('password'), "password exists");
        assert.equal(json['password'], user.password, 'expexted password');
        assert.strictEqual(typeof json.password, 'string', "password has corect type");
 
        assert.ok(json.hasOwnProperty('_createdOn'), "_createdOn exists");
        assert.strictEqual(typeof json._createdOn, 'number', "password has corect type");
 
        assert.ok(json.hasOwnProperty('_id'), "_id exists");
        assert.strictEqual(typeof json._id, 'string', "_id has corect type");
 
        assert.ok(json.hasOwnProperty('accessToken'), "accessToken exists");
        assert.strictEqual(typeof json.accessToken, 'string', "accessToken has corect type");
 
        token = json['accessToken'];
        userId = json['_id'];
        sessionStorage.setItem('event-user', JSON.stringify(user));
    });
 
});
 
 
QUnit.module("Album functionalities", () => {
    QUnit.test("get all albums", async(assert) => {
    let path = 'data/albums';
    let queryParams = '?sortBy=_createdOn%20desc&distinct=name';
 
    let response = await fetch(baseUrl + path + queryParams);
    let json = await response.json();
 
 
    assert.ok(response.ok, "Response is ok");
    assert.ok(Array.isArray(json), "response is array");
 
    json.forEach(jsonData => {
     assert.ok(jsonData.hasOwnProperty('artist'), "Author exists");
     assert.strictEqual(typeof jsonData.artist, 'string', "artist is from correct type");
 
     assert.ok(jsonData.hasOwnProperty('genre'), "date exists");
     assert.strictEqual(typeof jsonData.genre, 'string', "genre is from correct type");
 
     assert.ok(jsonData.hasOwnProperty('description'), "description exists");
     assert.strictEqual(typeof jsonData.description, 'string', "description is from correct type");
 
     assert.ok(jsonData.hasOwnProperty('imgUrl'), "imageUrl exists");
     assert.strictEqual(typeof jsonData.imgUrl, 'string', "imgUrl is from correct type");
 
     assert.ok(jsonData.hasOwnProperty('name'), "name exists");
     assert.strictEqual(typeof jsonData.name, 'string',"name is from correct type");
 
     assert.ok(jsonData.hasOwnProperty('_createdOn'), "_createdOn exists");
     assert.strictEqual(typeof jsonData._createdOn, 'number', "_createdOn is from correct type");
 
     assert.ok(jsonData.hasOwnProperty('_id'), "_id exists");
     assert.strictEqual(typeof jsonData._id, 'string', "_id is from correct type");
 
     assert.ok(jsonData.hasOwnProperty('_ownerId'), "_ownerId exists");
     assert.strictEqual(typeof jsonData._ownerId, 'string', "_ownerId is from correct type");
 
    }); 
 
 
 
    });
    QUnit.test("Create album", async (assert) =>{
        let path = "data/albums";
        let random = Math.floor(Math.random() * 10000);
 
        myAlbum.name = `Random album title ${random}`;
        myAlbum.description = `Random description ${random}`;
 
        let response = await fetch(baseUrl + path, {
            method: "POST",
            headers: {
                'content-type': 'application/json',
                'X-Authorization': token
            },
            body: JSON.stringify(myAlbum)
        })
        let jsonData = await response.json();
 
 
        assert.ok(response.ok, "Response is ok");
 
        assert.ok(jsonData.hasOwnProperty('artist'), "artist exists");
        assert.strictEqual(jsonData.artist, myAlbum.artist, "artist is expected");
        assert.strictEqual(typeof jsonData.artist, 'string', "artist is from correct type");
 
        assert.ok(jsonData.hasOwnProperty('genre'), "genre exists");
        assert.strictEqual(jsonData.genre, myAlbum.genre, "genre is expected");
        assert.strictEqual(typeof jsonData.genre, 'string', "genre is from correct type");
 
        assert.ok(jsonData.hasOwnProperty('description'), "description exists");
        assert.strictEqual(jsonData.description, myAlbum.description, "description is expected");
        assert.strictEqual(typeof jsonData.description, 'string', "description is from correct type");
 
        assert.ok(jsonData.hasOwnProperty('imgUrl'), "imgUrl exists");
        assert.strictEqual(jsonData.imgUrl, myAlbum.imgUrl, "imgUrl is expected");
        assert.strictEqual(typeof jsonData.imgUrl, 'string', "imgUrl is from correct type");
 
        assert.ok(jsonData.hasOwnProperty('name'), "name exists");
        assert.strictEqual(jsonData.name, myAlbum.name, "name is expected");
        assert.strictEqual(typeof jsonData.name, 'string', "name is from correct type");
 
        assert.ok(jsonData.hasOwnProperty('price'), "price exists");
        assert.strictEqual(jsonData.price, myAlbum.price, "price is expected");
        assert.strictEqual(typeof jsonData.price, 'string', "price is from correct type");
 
        assert.ok(jsonData.hasOwnProperty('releaseDate'), "releaseDate exists");
        assert.strictEqual(jsonData.releaseDate, myAlbum.releaseDate, "releaseDate is expected");
        assert.strictEqual(typeof jsonData.releaseDate, 'string', "releaseDate is from correct type");
 
        assert.ok(jsonData.hasOwnProperty('_createdOn'), "_createdOn exists");
        assert.strictEqual(typeof jsonData._createdOn, 'number', "_createdOn is from correct type");
 
        assert.ok(jsonData.hasOwnProperty('_id'), "_id exists");
        assert.strictEqual(typeof jsonData._id, 'string', "_id is from correct type");
 
        assert.ok(jsonData.hasOwnProperty('_ownerId'), "_ownerId exists");
        assert.strictEqual(typeof jsonData._ownerId, 'string', "_ownerId is from correct type");
 
        lastCreatedAlbum = jsonData._id;
 
    })
    QUnit.test("Edit album", async (assert) =>{
    let path = 'data/albums';
    let random = Math.floor(Math.random() * 10000);
    myAlbum.title = `Edited title ${random}`;
 
    let response = await fetch(baseUrl + path + `/${lastCreatedAlbum}`, {
        method: "PUT",
        headers: {
            'content-type': 'application/json',
            'X-Authorization': token
        },
        body: JSON.stringify(myAlbum)
    })
    let jsonData = await response.json();
 
    assert.ok(response.ok, "Response is ok");
 
        assert.ok(jsonData.hasOwnProperty('artist'), "artist exists");
        assert.strictEqual(jsonData.artist, myAlbum.artist, "artist is expected");
        assert.strictEqual(typeof jsonData.artist, 'string', "artist is from correct type");
 
        assert.ok(jsonData.hasOwnProperty('genre'), "genre exists");
        assert.strictEqual(jsonData.genre, myAlbum.genre, "genre is expected");
        assert.strictEqual(typeof jsonData.genre, 'string', "genre is from correct type");
 
        assert.ok(jsonData.hasOwnProperty('description'), "description exists");
        assert.strictEqual(jsonData.description, myAlbum.description, "description is expected");
        assert.strictEqual(typeof jsonData.description, 'string', "description is from correct type");
 
        assert.ok(jsonData.hasOwnProperty('imgUrl'), "imgUrl exists");
        assert.strictEqual(jsonData.imgUrl, myAlbum.imgUrl, "imgUrl is expected");
        assert.strictEqual(typeof jsonData.imgUrl, 'string', "imgUrl is from correct type");
 
        assert.ok(jsonData.hasOwnProperty('name'), "name exists");
        assert.strictEqual(jsonData.name, myAlbum.name, "name is expected");
        assert.strictEqual(typeof jsonData.name, 'string', "name is from correct type");
 
        assert.ok(jsonData.hasOwnProperty('price'), "price exists");
        assert.strictEqual(jsonData.price, myAlbum.price, "price is expected");
        assert.strictEqual(typeof jsonData.price, 'string', "price is from correct type");
 
        assert.ok(jsonData.hasOwnProperty('releaseDate'), "releaseDate exists");
        assert.strictEqual(jsonData.releaseDate, myAlbum.releaseDate, "releaseDate is expected");
        assert.strictEqual(typeof jsonData.releaseDate, 'string', "releaseDate is from correct type");
 
        assert.ok(jsonData.hasOwnProperty('title'), "title exists");
        assert.strictEqual(jsonData.title, myAlbum.title, "title is expected");
        assert.strictEqual(typeof jsonData.title, 'string', "title is from correct type");
 
 
        assert.ok(jsonData.hasOwnProperty('_createdOn'), "_createdOn exists");
        assert.strictEqual(typeof jsonData._createdOn, 'number', "_createdOn is from correct type");
 
        assert.ok(jsonData.hasOwnProperty('_id'), "_id exists");
        assert.strictEqual(typeof jsonData._id, 'string', "_id is from correct type");
 
        assert.ok(jsonData.hasOwnProperty('_ownerId'), "_ownerId exists");
        assert.strictEqual(typeof jsonData._ownerId, 'string', "_ownerId is from correct type");
 
        assert.ok(jsonData.hasOwnProperty('_updatedOn'), "_updatedOn exists");
        assert.strictEqual(typeof jsonData._updatedOn, 'number', "_updatedOn is from correct type");
 
 
    })
    QUnit.test("Delete album", async (assert) => {
        let path = "data/albums";
 
        let response = await fetch(baseUrl + path + `/${lastCreatedAlbum}`, {
            method: "DELETE",
            headers: {
                'X-Authorization' : token
            }
 
        })
        assert.ok(response.ok)
 
    }
 
    )
});
var express = require('express')
var bodyParser = require('body-parser').json();
var cache = require('memory-cache');
var app = express();

app.put('/api/:id', bodyParser, function (req, res) {
    cache.put("data", req.body);
    res.status(200).send("ok");
});

app.get('/api/:id', function (req, res) {
    var data = cache.get("data");
    res.setHeader('Access-Control-Allow-Origin', '*');
    res.setHeader('Access-Control-Allow-Methods', 'GET, PUT, POST, DELETE, OPTIONS');
    res.setHeader('Access-Control-Allow-Headers', 'Accept, Content-Type, Origin');
    res.setHeader('Access-Control-Allow-Credentials', true);
    res.send(data);
});
app.listen(3000, function () {
    console.log('Example app listening on port 3000!')
});
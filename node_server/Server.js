var express = require('express')
var bodyParser = require('body-parser').json();
var cache = require('memory-cache');
var app = express();

app.put('/api/:id', bodyParser, function (req, res) {
    cache.put("data",req.body);
    res.status(200).send("ok");
});

app.get('/api/:id', function (req, res) {
    var data = cache.get("data");
    res.send(data);
});
app.listen(3000, function () {
    console.log('Example app listening on port 3000!')
});
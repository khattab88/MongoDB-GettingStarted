// sample .mongorc.js file

var _no_ = function(){print("NO !")};

// disable drop database
DB.prototype.dropDatabase = _no_;
db.dropDatabase = DB.prototype.dropDatabase;

// disable shutdown db server
DB.prototype.shutdownServer = _no_;
db.shutdownServer = DB.prototype.shutdownServer;

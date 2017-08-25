cd \mongo\

md \db1
md \db2
md \db3

@REM Primary
start "a" mongod --dbpath ./db1 --port 30000 --replSet "demo"

@REM Secondary
start "b" mongod --dbpath ./db2 --port 40000 --replSet "demo"

@REM Arbitrary
start "c" mongod --dbpath ./db3 --port 50000 --replSet "demo"
/*

• MD5 generates a 128-bit hash value. You can use CHAR(32) or BINARY(16)
• SHA-1 generates a 160-bit hash value. You can use CHAR(40) or BINARY(20)
• SHA-224 generates a 224-bit hash value. You can use CHAR(56) or BINARY(28)
• SHA-256 generates a 256-bit hash value. You can use CHAR(64) or BINARY(32)
• SHA-384 generates a 384-bit hash value. You can use CHAR(96) or BINARY(48)
• SHA-512 generates a 512-bit hash value. You can use CHAR(128) or BINARY(64)
• BCrypt generates an implementation-dependent 448-bit hash value. You might need CHAR(56), CHAR(60), CHAR(76), BINARY(56) or BINARY(60)

Aus <https://stackoverflow.com/questions/247304/what-data-type-to-use-for-hashed-password-field-and-what-length> 


*/

-- example:
SELECT HASHBYTES('SHA1', '123User!')

-- convert the hash into a NVARCHAR
DECLARE @test VARCHAR(40)
SET @test = master.dbo.fn_varbintohexsubstring(0, HashBytes('SHA1', '123User!'), 1, 0)

SELECT @test AS TestHash
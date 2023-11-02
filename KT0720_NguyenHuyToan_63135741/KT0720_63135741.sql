CREATE DATABASE KT0720_63135741;

USE KT0720_63135741;

CREATE TABLE Lop (
    MaLop NVARCHAR(10) PRIMARY KEY,
    TenLop NVARCHAR(50) NOT NULL
);

CREATE TABLE SinhVien (
    MaSV NVARCHAR(10) PRIMARY KEY,
    HoSV NVARCHAR(50) NOT NULL,
    TenSV NVARCHAR(50) NOT NULL,
    NgaySinh DATE,
    GioiTinh BIT DEFAULT(1),
    AnhSV NVARCHAR(MAX), 
    DiaChi NVARCHAR(255) NOT NULL,
    MaLop NVARCHAR(10) NOT NULL FOREIGN KEY REFERENCES Lop(MaLop)
	ON UPDATE CASCADE
	ON DELETE CASCADE
);

drop table SinhVien

INSERT INTO Lop VALUES
(N'DHLT17', N'Đại học liên thông khoá 2017'),
(N'DHLT18', N'Đại học liên thông khoá 2018'),
(N'DHLT19', N'Đại học liên thông khoá 2019');

INSERT INTO SinhVien VALUES
(N'17TH0100', N'Lê Thành', N'Duy', N'1995-01-01', 1,  N'17TH0100.jpg', N'Nha Trang, Khánh Hoà', N'DHLT17'),
(N'17TH0101', N'Nguyễn Mạnh', N'Hùng', N'1997-03-03', 1, N'17TH0101.jpg', N'Nha Trang, Khánh Hoà', N'DHLT19'),
(N'17TH0102', N'Lê Thị Mỹ', N'Châu', N'1994-09-12', 0, N'17TH0102.jpg', N'Nha Trang, Khánh Hoà', N'DHLT18');

CREATE PROCEDURE TimKiem
    @MaSV varchar(10) = NULL,
    @HoTen nvarchar(50) = NULL,
	@MaLop nvarchar(10)=NULL
AS
BEGIN
DECLARE @SqlStr NVARCHAR(4000),
		@ParamList nvarchar(2000)
SELECT @SqlStr = '
       SELECT * 
       FROM SinhVien
       WHERE  (1=1)
       '
    IF @MaSV <> ''
       SELECT @SqlStr = @SqlStr + '
              AND (MaSV = '''+@MaSV+''')
              '
	IF @HoTen IS NOT NULL
       SELECT @SqlStr = @SqlStr + '
              AND (HoSV+'' ''+TenSV LIKE N''%'+@HoTen+'%'')
			  '
	IF @MaLop IS NOT NULL
       SELECT @SqlStr = @SqlStr + '
              AND (MaLop LIKE ''%'+@MaLop+'%'')
			  '
	EXEC SP_EXECUTESQL @SqlStr
END;

drop PROCEDURE TimKiem


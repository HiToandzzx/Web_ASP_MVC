CREATE DATABASE ThiGK_63135741;

USE ThiGK_63135741;

CREATE TABLE LoaiDocGia (
    MaLoaiDG NVARCHAR(10) PRIMARY KEY,
    TenLoaiDG NVARCHAR(50) NOT NULL
);

CREATE TABLE DocGia (
    MaDG VARCHAR(10) PRIMARY KEY,
    HoDG NVARCHAR(50) NOT NULL,
    TenDG NVARCHAR(50) NOT NULL,
    NgaySinh DATE,
    GioiTinh BIT DEFAULT(1),
    AnhDG NVARCHAR(MAX), 
	Email NVARCHAR(50) NOT NULL,
	MaLoaiDG NVARCHAR(10) NOT NULL FOREIGN KEY REFERENCES LoaiDocGia(MaLoaiDG)
	ON UPDATE CASCADE
	ON DELETE CASCADE
);

INSERT INTO LoaiDocGia VALUES
(N'SV', N'Sinh viên'),
(N'CSV', N'Cựu sinh viên'),
(N'GV', N'Giáo viên');

INSERT INTO DocGia VALUES
('16TH001', N'Trần Thị Tú', N'Anh', N'1995-05-12', 0,  N'16001.png', N'anhttt@gmail.com', N'SV'),
('16TH002', N'Nguyễn Mạnh', N'Hùng', N'1995-10-24', 1, N'16002.gif', N'cuongtd@gmail.com', N'SV'),
('16TH003', N'Lê Thị Mỹ', N'Châu', N'1994-09-12', 0, N'16003.jpg', N'chaultm@gmail.com', N'CSV');

CREATE PROCEDURE TimKiem
    @MaDG NVARCHAR(10) = NULL,
    @HoTen NVARCHAR(50) = NULL,
    @MaLoaiDG NVARCHAR(10) = NULL
AS
BEGIN
    DECLARE @SqlStr NVARCHAR(4000),
			@ParamList nvarchar(2000)
    SELECT @SqlStr = '
       SELECT * 
       FROM DocGia
       WHERE 1=1'

    IF @MaDG <> ''
       SELECT @SqlStr = @SqlStr + '
              AND (MaDG = '''+@MaDG+''')'

    IF @HoTen IS NOT NULL
       SELECT @SqlStr = @SqlStr + '
              AND (HoDG + '' '' + TenDG LIKE N''%' + @HoTen + '%'')'

    IF @MaLoaiDG IS NOT NULL
       SELECT @SqlStr = @SqlStr + '
              AND (MaLoaiDG LIKE ''%' + @MaLoaiDG + '%'')'

    EXEC sp_executesql @SqlStr
END;

drop PROCEDURE TimKiem
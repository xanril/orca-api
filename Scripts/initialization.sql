-- Create Movie table
CREATE TABLE Movies
(
    Id INT IDENTITY PRIMARY KEY,
    TmdbId INT,
    Title NVARCHAR(128),
    Tagline NVARCHAR(128),
    Overview NVARCHAR(256),
    Runtime INT,
    PosterUrl NVARCHAR(128),
    BackdropUrl NVARCHAR(128),
    ReleaseDate DATETIME
)

-- Create Cinema table
CREATE TABLE Cinemas
(
    Id INT IDENTITY PRIMARY KEY,
    Name NVARCHAR(128) NOT NULL,
    Location NVARCHAR(128)
)

-- Create Room table
CREATE TABLE Rooms
(
    Id INT IDENTITY PRIMARY KEY,
    Name NVARCHAR(128) NOT NULL,
    CinemaId INT REFERENCES Cinemas (Id) ON DELETE CASCADE
)

-- Create Schedule table
CREATE TABLE Schedules
(
    Id INT IDENTITY PRIMARY KEY,
    CinemaId INT REFERENCES Cinemas (Id) ON DELETE CASCADE,
    RoomId INT REFERENCES Rooms (Id),
    MovieId INT REFERENCES Movies (Id) ON DELETE CASCADE,
    DayOfWeek INT NOT NULL,
    StartTime DATETIME,
    EndTime DATETIME,
    TicketPrice DECIMAL
)

DROP TABLE Schedules
DROP TABLE Rooms
DROP TABLE Cinemas
DROP TABLE Movies
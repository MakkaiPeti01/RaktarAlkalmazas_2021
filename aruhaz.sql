-- phpMyAdmin SQL Dump
-- version 5.0.2
-- https://www.phpmyadmin.net/
--
-- Gép: 127.0.0.1
-- Létrehozás ideje: 2021. Ápr 22. 12:38
-- Kiszolgáló verziója: 10.4.11-MariaDB
-- PHP verzió: 7.4.4

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Adatbázis: `aruhaz`
--

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `felhasznalok`
--

CREATE TABLE `felhasznalok` (
  `id` int(11) NOT NULL,
  `felhasznalonev` varchar(15) COLLATE utf8mb4_hungarian_ci NOT NULL,
  `jelszo` varchar(15) COLLATE utf8mb4_hungarian_ci NOT NULL,
  `jogkor` varchar(20) COLLATE utf8mb4_hungarian_ci NOT NULL,
  `jogkor_id` int(11) NOT NULL,
  `szemelyNeve` varchar(30) COLLATE utf8mb4_hungarian_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_hungarian_ci;

--
-- A tábla adatainak kiíratása `felhasznalok`
--

INSERT INTO `felhasznalok` (`id`, `felhasznalonev`, `jelszo`, `jogkor`, `jogkor_id`, `szemelyNeve`) VALUES
(1, 'peti', 'petike', 'admin', 1, 'Makkai Péter'),
(2, 'nempetike', 'petike2', 'felhasznalo', 0, 'Ő is Makkai Péter'),
(3, 'kassza01', 'kassza01', 'felhasznalo', 0, 'Kasszás Erzsi');

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `hustipusok`
--

CREATE TABLE `hustipusok` (
  `id` int(11) NOT NULL,
  `nev` varchar(30) COLLATE utf8mb4_hungarian_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_hungarian_ci;

--
-- A tábla adatainak kiíratása `hustipusok`
--

INSERT INTO `hustipusok` (`id`, `nev`) VALUES
(1, 'sertés'),
(2, 'marha'),
(3, 'birka'),
(4, 'kacsa'),
(5, 'liba'),
(6, 'tyúk');

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `kassza`
--

CREATE TABLE `kassza` (
  `id` int(11) NOT NULL,
  `kassza_neve` varchar(20) COLLATE utf8mb4_hungarian_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_hungarian_ci;

--
-- A tábla adatainak kiíratása `kassza`
--

INSERT INTO `kassza` (`id`, `kassza_neve`) VALUES
(1, 'Admin kassza'),
(2, 'Peti Kassza'),
(3, 'Erzsi Kassza');

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `kategoriak`
--

CREATE TABLE `kategoriak` (
  `id` int(11) NOT NULL,
  `kategoria_nev` varchar(30) COLLATE utf8mb4_hungarian_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_hungarian_ci;

--
-- A tábla adatainak kiíratása `kategoriak`
--

INSERT INTO `kategoriak` (`id`, `kategoria_nev`) VALUES
(1, 'Hús'),
(2, 'Zöldség_Gyümölcs'),
(3, 'Pékáru'),
(4, 'Tejtermék'),
(5, 'Szeszes_ital (Alkohol)'),
(6, 'Tészták'),
(7, 'Papír_írószer'),
(9, 'Drogéria');

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `sumseged`
--

CREATE TABLE `sumseged` (
  `nev` varchar(255) COLLATE utf8mb4_hungarian_ci NOT NULL,
  `osszeg` int(11) NOT NULL,
  `db` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_hungarian_ci;

--
-- A tábla adatainak kiíratása `sumseged`
--

INSERT INTO `sumseged` (`nev`, `osszeg`, `db`) VALUES
('Csabai füstölt vastagkolbász', 1000, 1);

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `termekek`
--

CREATE TABLE `termekek` (
  `id` int(11) NOT NULL,
  `termek_nev` varchar(30) COLLATE utf8mb4_hungarian_ci NOT NULL,
  `kivitel` varchar(30) COLLATE utf8mb4_hungarian_ci NOT NULL,
  `kategoria_id` int(11) NOT NULL,
  `tipus_id` int(11) NOT NULL,
  `nettoAr` int(11) NOT NULL,
  `bruttoAr` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_hungarian_ci;

--
-- A tábla adatainak kiíratása `termekek`
--

INSERT INTO `termekek` (`id`, `termek_nev`, `kivitel`, `kategoria_id`, `tipus_id`, `nettoAr`, `bruttoAr`) VALUES
(1, 'Csabai füstölt vastagkolbász', 'csomag (250g)', 1, 1, 730, 1000),
(2, 'Fürtös Paradicsom', 'csomag (1 kg)', 2, 18, 445, 610),
(3, 'Berceli Szeletelt Fehér Kenyér', 'csomag (1 kg)', 3, 19, 314, 430),
(4, 'Riska tej', 'doboz (1 liter)', 4, 21, 256, 350),
(5, 'Soproni sör', 'doboz (0,5L)', 5, 11, 212, 290),
(6, 'Coop Levestészta', 'csomag(250g)', 6, 10, 274, 375),
(7, 'Nyomtatópapír', 'csomag (500 db)', 7, 24, 1088, 1490),
(8, 'Rexona Férfi dezodor', 'Fémtároló(150ml)', 8, 27, 434, 595);

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `termektipusok`
--

CREATE TABLE `termektipusok` (
  `id` int(11) NOT NULL,
  `tipus` varchar(30) COLLATE utf8mb4_hungarian_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_hungarian_ci;

--
-- A tábla adatainak kiíratása `termektipusok`
--

INSERT INTO `termektipusok` (`id`, `tipus`) VALUES
(1, 'sertés'),
(2, 'marha'),
(3, 'birka'),
(4, 'kacsa'),
(5, 'liba'),
(6, 'tyúk'),
(7, 'spagetti'),
(8, 'csőtészta'),
(9, 'kockatészta'),
(10, 'levestészta'),
(11, 'sör (sima)'),
(12, 'citromos'),
(13, 'málnás'),
(14, 'paprika'),
(15, 'Kaliforniai paprika'),
(16, 'Vöröshagyma'),
(17, 'Lilahagyma'),
(18, 'paradicsom'),
(19, 'Fehér kenyér'),
(20, 'Barna Kenyér'),
(21, 'Tej'),
(22, 'Tejföl'),
(23, 'Túró'),
(24, 'papir'),
(25, 'toll/ceruza'),
(26, 'patron'),
(27, 'dezodor (izzadásgátló)'),
(28, 'parfüm'),
(29, 'arckrém/maszk');

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `vasarlas`
--

CREATE TABLE `vasarlas` (
  `id` int(11) NOT NULL,
  `termekek` varchar(255) COLLATE utf8mb4_hungarian_ci NOT NULL,
  `osszeg` int(11) NOT NULL,
  `datum` varchar(20) COLLATE utf8mb4_hungarian_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_hungarian_ci;

--
-- A tábla adatainak kiíratása `vasarlas`
--

INSERT INTO `vasarlas` (`id`, `termekek`, `osszeg`, `datum`) VALUES
(6, 'Csabai füstölt vastagkolbász', 1430, '2021.04.10'),
(7, 'Csabai füstölt vastagkolbász', 2490, '2021.04.12'),
(8, 'Coop Levestészta', 1075, '2021.04.12'),
(9, 'Fürtös Paradicsom', 2470, '2021.04.15'),
(10, 'Fürtös Paradicsom', 1595, '2021.04.22');

--
-- Indexek a kiírt táblákhoz
--

--
-- A tábla indexei `felhasznalok`
--
ALTER TABLE `felhasznalok`
  ADD PRIMARY KEY (`id`);

--
-- A tábla indexei `hustipusok`
--
ALTER TABLE `hustipusok`
  ADD PRIMARY KEY (`id`);

--
-- A tábla indexei `kassza`
--
ALTER TABLE `kassza`
  ADD PRIMARY KEY (`id`);

--
-- A tábla indexei `kategoriak`
--
ALTER TABLE `kategoriak`
  ADD PRIMARY KEY (`id`);

--
-- A tábla indexei `termekek`
--
ALTER TABLE `termekek`
  ADD PRIMARY KEY (`id`),
  ADD KEY `FK_termekek_kategoriak_id` (`kategoria_id`),
  ADD KEY `FK_termekek_termektipusok_id` (`tipus_id`);

--
-- A tábla indexei `termektipusok`
--
ALTER TABLE `termektipusok`
  ADD PRIMARY KEY (`id`);

--
-- A tábla indexei `vasarlas`
--
ALTER TABLE `vasarlas`
  ADD PRIMARY KEY (`id`);

--
-- A kiírt táblák AUTO_INCREMENT értéke
--

--
-- AUTO_INCREMENT a táblához `felhasznalok`
--
ALTER TABLE `felhasznalok`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT a táblához `hustipusok`
--
ALTER TABLE `hustipusok`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;

--
-- AUTO_INCREMENT a táblához `kassza`
--
ALTER TABLE `kassza`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT a táblához `kategoriak`
--
ALTER TABLE `kategoriak`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=10;

--
-- AUTO_INCREMENT a táblához `termekek`
--
ALTER TABLE `termekek`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=23;

--
-- AUTO_INCREMENT a táblához `termektipusok`
--
ALTER TABLE `termektipusok`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=31;

--
-- AUTO_INCREMENT a táblához `vasarlas`
--
ALTER TABLE `vasarlas`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=11;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;

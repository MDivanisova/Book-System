ALTER TABLE Favorite
ADD CONSTRAINT FavoritePriceNonNegative CHECK (Price >= 0);
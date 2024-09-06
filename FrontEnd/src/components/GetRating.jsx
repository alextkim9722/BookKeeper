export default function GetRating(rating)
{
    let ratingString = '';

    for(let i = 0;i < rating;i++)
    {
        ratingString = ratingString + '★';
    }

    for(let i = 0;i < (10 - rating);i++)
    {
        ratingString = ratingString + '☆';
    }

    return ratingString;
}
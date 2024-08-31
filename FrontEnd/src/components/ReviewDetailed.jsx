import { useEffect, useState } from 'react'
import './ReviewDetailed'

export default function ReviewDetailed(props)
{
    const [review, setReview] = useState({});

    useEffect(() => {
        setReview(props.review);
    }, [props.review]);

    return (
        <>
        <div style={{backgroundColor: 'rgb(200, 200, 200)'}}>
            <div> <p style={{width: '900px', wordWrap: 'normal', overflow: 'hidden', overflowWrap: 'break-word'}}>{review.description}</p> </div>
            <div> <p >{review.rating}</p> </div>
            <div> <p>{review.date_submitted}</p> </div>
        </div>
        </>
    )
}
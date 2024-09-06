import { useEffect, useState } from 'react'

import GetRating from '../GetRating';

import globalStyles from '../../Global.module.css'
import styles from './ReviewCrunched.module.css'

export default function ReviewCrunched(props)
{
    const [username, setUsername] = useState('');
    const [review, setReview] = useState({});

    useEffect(() => {
        setUsername(props.username);
        setReview(props.review);
    }, [props.review, props.username]);

    return (
        <>
        <div className={`${globalStyles.breakOverlap} ${styles.reviewBox}`} style={{padding:'20px'}}>
            <div style={{fontWeight:'bold'}}> 
                <span>{username}</span>
                <span style={{position:'relative', left: '200px'}}>{GetRating(review.rating)}</span>
                <span style={{float:'right'}}>{review.date_submitted}</span>
            </div>
            <div style={{marginTop:'10px'}}>{review.description}</div>
        </div>
        </>
    )
}
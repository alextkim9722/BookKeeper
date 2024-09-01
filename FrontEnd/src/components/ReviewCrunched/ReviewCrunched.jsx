import { useEffect, useState } from 'react'

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
        <div className={`${globalStyles.breakOverlap} ${styles.reviewBox}`}>
            <div> 
                <span>{username}</span>
                <span style={{position:'relative', left: '200px'}}>{review.rating}/10</span>
                <span style={{float:'right'}}>{review.date_submitted}</span>
            </div>
            <div>{review.description}</div>
        </div>
        </>
    )
}
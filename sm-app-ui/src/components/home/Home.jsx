import React from 'react'
import './home.scss'
import PostingModule from './post-module/PostingModule'
import TimelineModule from './timeline-module/TimelineModule'


const Home = () => {
    return (
        <div className='main-timeline'>
            <PostingModule />
            <TimelineModule />
            <TimelineModule />
            <TimelineModule />
            <TimelineModule />
            <TimelineModule />
            <TimelineModule />
            <TimelineModule />

        </div >
    )
}

export default Home
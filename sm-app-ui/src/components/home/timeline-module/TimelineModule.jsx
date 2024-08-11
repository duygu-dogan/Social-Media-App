import React from 'react'

const TimelineModule = () => {
    return (
        <div className='timeline-post flex mx-10 gap-3 my-5'>
            <div className='w-1/12 flex justify-center'>
                <img className="rounded-full h-10 w-10 hover:brightness-75" alt="recommended-user-pic" src='https://abs.twimg.com/sticky/default_profile_images/default_profile_bigger.png' />
            </div>
            <div className='w-11/12'>
                <div className='flex gap-2 items-center'>
                    <strong>Username</strong>
                    <span className='text-gray-400'>@username</span>
                    <span className='text-gray-400'>&middot;</span>
                    <span className='text-gray-400'>Time</span>
                </div>
                <div>
                    <p>Post content</p>
                </div>
                <div className='flex w-full justify-between'>
                    <div>
                        <button>Comment</button>
                        <button>Retweet</button>
                        <button>Like</button>
                    </div>
                    <div>
                        <button>Bookmark</button>
                        <button>Share</button>
                    </div>

                </div>
            </div>
        </div>
    )
}

export default TimelineModule
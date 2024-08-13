import React from 'react'
import './timelinemodule.scss'
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import { faBookmark, faComment, faShare } from '@fortawesome/free-solid-svg-icons'
import { faRecycle } from '@fortawesome/free-solid-svg-icons/faRecycle'
import { faHeart } from '@fortawesome/free-solid-svg-icons/faHeart'

const TimelineModule = () => {
    return (
        <div className='timeline-post '>
            <div className='flex mx-10 gap-3 my-5'>
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
                        <div className='text-justify'>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed consectetur nisi odio, quis vestibulum libero congue vulputate. Ut non neque non ipsum accumsan tempus. Praesent suscipit finibus augue vitae consequat. Pellentesque mollis, mauris at imperdiet porttitor, risus laoreet.</div>
                    </div>
                    <div className='flex justify-between mt-4 text-lg'>
                        <div className='flex w-8/12 justify-between'>
                            <button>
                                <div><FontAwesomeIcon icon={faComment} /> #</div>
                            </button>
                            <button>
                                <div><FontAwesomeIcon icon={faRecycle} /> #</div>
                            </button>
                            <button>
                                <div><FontAwesomeIcon icon={faHeart} /> #</div>
                            </button>
                        </div>
                        <div className='flex w-2/12 justify-end gap-4'>
                            <button>
                                <div><FontAwesomeIcon icon={faBookmark} /></div>
                            </button>
                            <button>
                                <div><FontAwesomeIcon icon={faShare} /></div>
                            </button>
                        </div>

                    </div>
                </div>
            </div>
        </div>
    )
}

export default TimelineModule
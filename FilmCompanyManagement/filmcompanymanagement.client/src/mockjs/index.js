import Mock, { Random } from 'mockjs'
Mock.mock('/data/userdata', 'get', {
    name: Random.cname(),
})

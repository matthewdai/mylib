export class ProjectService {

    getProjects(){
        return [
            {name:"Learning Angular 2", author:"Matthew, Dai", active:true}, 
            {name:"Pro typeScript", author:"Cindy, Xie", active:false}, 
            {name:"ASP.NET", author:"Ronger, Dai", active:true}, 
            {name:"Pro MVC 5", author:"Zheliang, Liu", active:true}
        ];
    }
}
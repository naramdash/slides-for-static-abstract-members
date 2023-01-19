import{f as d,h as _,j as h,ag as p,c as m,ah as u,l as n,ai as t,aj as s,y as o,F as f,ak as g,al as v,am as b,n as r,an as x,ao as y,m as k,ap as N,aq as w,_ as P}from"./nav-ca38e0f2.js";import{N as V}from"./NoteViewer-df1b0f9a.js";import{u as j}from"./index-741290ac.js";const S={class:"m-4"},L={class:"mb-10"},T={class:"text-4xl font-bold mt-2"},B={class:"opacity-50"},C={class:"text-lg"},H={class:"font-bold flex gap-2"},z={class:"opacity-50"},D=t("div",{class:"flex-auto"},null,-1),F={key:0,class:"border-gray-400/50 mb-8"},M=d({__name:"PresenterPrint",setup(q){_(h),p(`
@page {
  size: A4;
  margin-top: 1.5cm;
  margin-bottom: 1cm;
}
* {
  -webkit-print-color-adjust: exact;
}
html,
html body,
html #app,
html #page-root {
  height: auto;
  overflow: auto !important;
}
`),j({title:`Notes - ${m.title}`});const l=u(()=>b.slice(0,-1).map(a=>{var i;return(i=a.meta)==null?void 0:i.slide}).filter(a=>a!==void 0&&a.notesHTML!==""));return(a,i)=>(r(),n("div",{id:"page-root",style:v(o(w))},[t("div",S,[t("div",L,[t("h1",T,s(o(m).title),1),t("div",B,s(new Date().toLocaleString()),1)]),(r(!0),n(f,null,g(o(l),(e,c)=>(r(),n("div",{key:c,class:"flex flex-col gap-4 break-inside-avoid-page"},[t("div",null,[t("h2",C,[t("div",H,[t("div",z,s(e==null?void 0:e.no)+"/"+s(o(x)),1),y(" "+s(e==null?void 0:e.title)+" ",1),D])]),k(V,{"note-html":e.notesHTML,class:"max-w-full"},null,8,["note-html"])]),c<o(l).length-1?(r(),n("hr",F)):N("v-if",!0)]))),128))])],4))}}),W=P(M,[["__file","/home/runner/work/slides-for-static-abstract-members/slides-for-static-abstract-members/node_modules/@slidev/client/internals/PresenterPrint.vue"]]);export{W as default};

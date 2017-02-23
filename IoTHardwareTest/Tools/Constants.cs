﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoTHardwareTest.Tools
{
    public static class Constants
    {
        public const string AUDIO_TEST_PAGE_ICON = "M13.1445827484131,6.65641736984253L13.1445827484131,14.5923376083374 13.2763261795044,14.5324192047119 15.177173614502,12.909441947937 15.7196292877197,11.8153018951416 15.9132556915283,10.6240644454956 15.7196292877197,9.43317985534668 15.177173614502,8.33922576904297 13.2763261795044,6.71633577346802 13.1445827484131,6.65641736984253z M1.87494325637817,6.24921751022339L1.43308162689209,6.43257141113281 1.24996221065521,6.8741979598999 1.24996221065521,13.1240015029907 1.43308162689209,13.5656290054321 1.87494325637817,13.7489824295044 4.4117317199707,13.7489824295044 4.4117317199707,6.24921751022339 1.87494325637817,6.24921751022339z M14.9832534790039,3.12334561347961L15.2782354354858,3.20792007446289 17.2631511688232,4.68429136276245 18.7476863861084,6.46862411499023 19.6779365539551,8.47631072998047 20,10.6227436065674 19.6779365539551,12.7691774368286 18.7476863861084,14.7768640518188 17.2631511688232,16.5611953735352 15.2782354354858,18.0375671386719 14.9638681411743,18.1225662231445 14.4238805770874,17.812572479248 14.3588829040527,17.3386783599854 14.648250579834,16.9575939178467 16.3889026641846,15.6781806945801 17.6766948699951,14.1588315963745 18.4757080078125,12.4551515579224 18.7500286102295,10.6227436065674 18.4757080078125,8.7903356552124 17.6766948699951,7.08665609359741 16.3889026641846,5.567307472229 14.648250579834,4.28789472579956 14.3588829040527,3.90680980682373 14.4238805770874,3.43291473388672 14.9832534790039,3.12334561347961z M11.3385238647461,1.2499942779541L10.9734020233154,1.37109136581421 5.66170310974121,5.5897274017334 5.66170310974121,14.3563890457153 11.0158967971802,18.658145904541 11.6252565383911,18.6819095611572 11.8946228027344,18.2200374603271 11.8946228027344,16.2540893554688 11.8746271133423,16.2588748931885 11.8746271133423,4.98925495147705 11.8946228027344,4.99405527114868 11.8946228027344,1.77795684337616 11.6271257400513,1.31669533252716 11.390567779541,1.25059258937836 11.3385238647461,1.2499942779541z M11.3744974136353,4.18177523897612E-06L12.2208642959595,0.216754391789436 12.8997459411621,0.86516535282135 13.1445827484131,1.77795684337616 13.1445827484131,5.31845474243164 13.3518295288086,5.39211177825928 14.8822689056396,6.24683427810669 16.0885543823242,7.47720098495483 16.8793239593506,8.97301197052002 17.1632175445557,10.6240644454956 16.8793239593506,12.2754526138306 16.0885543823242,13.7714500427246 14.8822689056396,15.0018978118896 13.3518295288086,15.8566408157349 13.1445827484131,15.9302978515625 13.1445827484131,18.2200374603271 12.898494720459,19.1349411010742 12.2164878845215,19.7831478118896 11.3590087890625,19.9968795776367 10.279673576355,19.6662673950195 4.46551561355591,14.9989433288574 1.87494325637817,14.9989433288574 0.54982715845108,14.4491167068481 0,13.1240015029907 0,6.8741979598999 0.54982715845108,5.54908323287964 1.87494325637817,4.99925708770752 4.4117317199707,4.99925708770752 4.4117317199707,4.985999584198 10.2421770095825,0.359230071306229 11.3744974136353,4.18177523897612E-06z";
        public const string UART_TEST_PAGE_ICON = "M2.37066388130188,5.00015830993652L2.26758718490601,5.0052661895752 1.54525947570801,5.36270523071289 1.25066590309143,6.10644578933716 1.25066590309143,11.314733505249 1.58278632164001,12.0966329574585 2.38320350646973,12.4210176467896 2.96759796142578,12.4210176467896 3.82325077056885,12.7772789001465 4.17826271057129,13.6360559463501 4.17826271057129,14.4098300933838 6.48333883285522,14.3754549026489 6.44458723068237,13.6360559463501 6.44458723068237,13.6354312896729 6.7996768951416,12.7772006988525 7.65587711334229,12.4210176467896 8.24277114868164,12.4210176467896 9.04350090026855,12.0966329574585 9.37593364715576,11.314733505249 9.37593364715576,6.10644578933716 9.08105659484863,5.36270523071289 8.35839462280273,5.0052661895752 8.25531196594238,5.00015830993652 2.37066388130188,5.00015830993652z M5.93831968307495,1.25003945827484L5.93831968307495,3.74949789047241 8.12588787078857,3.74949789047241 8.12588787078857,1.25003945827484 5.93831968307495,1.25003945827484z M2.50071096420288,1.25003945827484L2.50071096420288,3.74949789047241 4.68828010559082,3.74949789047241 4.68828010559082,1.25003945827484 2.50071096420288,1.25003945827484z M1.25067162513733,0L4.68828010559082,0 5.93831968307495,0 9.37592792510986,0 9.37592792510986,4.03340196609497 9.37795543670654,4.03443002700806 10.2805347442627,4.8853006362915 10.6259746551514,6.10644578933716 10.6259746551514,11.314733505249 10.4384393692017,12.2311000823975 9.92728042602539,12.9801759719849 9.16966724395752,13.4856119155884 8.24277114868164,13.6710567474365 7.69462871551514,13.6710567474365 7.69400310516357,14.4110803604126 7.33899164199829,15.2693099975586 6.48333883285522,15.6254930496216 6.11239528656006,15.6254930496216 6.11298131942749,15.6314573287964 6.5040397644043,16.8738746643066 7.2893648147583,17.8619632720947 8.38020992279053,18.5144329071045 9.68782901763916,18.7499866485596 10.2534971237183,18.7499866485596 11.5618104934692,18.4945945739746 12.6445302963257,17.7891979217529 13.4066143035889,16.7249698638916 13.7530241012573,15.3930778503418 13.7511167526245,11.8243007659912 13.9532260894775,10.4791994094849 14.5278997421265,9.24888515472412 16.6050186157227,7.42488145828247 16.845365524292,7.36405897140503 16.8908805847168,7.36355066299438 17.4394035339355,7.71737670898438 17.4659080505371,8.19457149505615 17.1469573974609,8.55177307128906 15.5851230621338,9.91357612609863 15.0011720657349,11.8243007659912 15.0011720657349,15.4393329620361 14.5313892364502,17.2572555541992 13.4976100921631,18.7021732330322 12.0286931991577,19.6558361053467 10.2534971237183,20 9.68782901763916,20 7.92664384841919,19.6816711425781 6.45739078521729,18.7999496459961 5.3995623588562,17.4647350311279 4.87265157699585,15.7859268188477 4.85691928863525,15.6254930496216 4.13951110839844,15.6254930496216 3.28331112861633,15.2689189910889 2.92822098731995,14.4098300933838 2.92822098731995,13.6360559463501 2.38320350646973,13.6710567474365 1.45630764961243,13.4856119155884 0.698694825172424,12.9801759719849 0.187535434961319,12.2311000823975 0,11.314733505249 0,6.10644578933716 0.345440477132797,4.8853006362915 1.24801969528198,4.03443002700806 1.25067162513733,4.03308486938477 1.25067162513733,0z";
        public const string GPIO_TEST_PAGE_ICON = "M2.49999666213989,1.24999833106995L1.6164824962616,1.6164824962616 1.24999833106995,2.49999666213989 1.6164824962616,3.38351130485535 2.49999666213989,3.74999499320984 3.38351130485535,3.38351130485535 3.74999499320984,2.49999666213989 3.38351130485535,1.6164824962616 2.49999666213989,1.24999833106995z M2.49999666213989,0L3.47228074073792,0.196738064289093 4.2670259475708,0.732967913150787 4.80325555801392,1.52771294116974 4.99999332427979,2.49999666213989 4.50275611877441,3.99494171142578 3.24269104003906,4.88743019104004 3.12499570846558,4.92091941833496 3.12499570846558,16.874979019165 15.6250057220459,16.874979019165 15.6250057220459,14.999981880188 20,17.4125080108643 15.6250057220459,19.9999752044678 15.6250057220459,18.1249752044678 1.87499749660492,18.1249752044678 1.87499749660492,4.92091941833496 1.75730228424072,4.88743019104004 0.497237503528595,3.99494171142578 0,2.49999666213989 0.196738064289093,1.52771294116974 0.732967913150787,0.732967913150787 1.52771294116974,0.196738064289093 2.49999666213989,0z";
        public const string I2C_TEST_PAGE_ICON = "M16.2500095367432,6.2499852180481L16.2500095367432,8.74997901916504 18.7500038146973,8.74997901916504 18.7500038146973,6.2499852180481 16.2500095367432,6.2499852180481z M2.49999403953552,1.24999701976776L1.61648082733154,1.61648035049438 1.24999701976776,2.49999403953552 1.61648082733154,3.38350772857666 2.49999403953552,3.74999094009399 3.38350772857666,3.38350772857666 3.74999094009399,2.49999403953552 3.38350772857666,1.61648035049438 2.49999403953552,1.24999701976776z M2.49999403953552,0L3.47227716445923,0.196737870573997 4.26702117919922,0.732967138290405 4.80325031280518,1.52771139144897 4.99998807907104,2.49999403953552 4.50275135040283,3.9949369430542 3.24268770217896,4.8874249458313 3.12499260902405,4.92091417312622 3.12499260902405,6.87498378753662 15.0000133514404,6.87498378753662 15.0000133514404,4.99998807907104 20,4.99998807907104 20,9.99997615814209 18.1250057220459,9.99997615814209 18.1250057220459,15.6249551773071 19.9999713897705,15.6249551773071 17.5874881744385,19.9999446868896 14.9999847412109,15.6249551773071 16.8750095367432,15.6249551773071 16.8750095367432,9.99997615814209 15.0000133514404,9.99997615814209 15.0000133514404,8.12498092651367 1.874995470047,8.12498092651367 1.874995470047,4.92091417312622 1.75730049610138,4.8874249458313 0.497236996889114,3.9949369430542 0,2.49999403953552 0.196737870573997,1.52771139144897 0.732967138290405,0.732967138290405 1.52771139144897,0.196737870573997 2.49999403953552,0z";
        public const string NETWORK_TEST_PAGE_ICON = "M7.08311986923218,13.125L8.54123973846436,13.125 8.54123973846436,14.375 7.08311986923218,14.375 7.08311986923218,13.125z M4.375,13.125L5.625,13.125 5.625,14.375 4.375,14.375 4.375,13.125z M11.25,11.2500047683716L11.25,16.2500038146973 18.75,16.2500038146973 18.75,11.2500047683716 11.25,11.2500047683716z M4.375,10.8756313323975L5.625,10.8756313323975 5.625,12.0006322860718 4.375,12.0006322860718 4.375,10.8756313323975z M14.375,10L15.625,10 20,10.0000047683716 20,17.5000038146973 15.625,17.5000038146973 15.625,18.75 16.875,18.75 16.875,20 13.1318845748901,20 13.1318845748901,18.75 14.375,18.75 14.375,17.5000038146973 10,17.5000038146973 10,14.375 10,13.125 10,10.0000047683716 14.375,10z M14.375,7.75000667572021L15.625,7.75000667572021 15.625,8.87563133239746 14.375,8.87563133239746 14.375,7.75000667572021z M14.375,5.50000715255737L15.625,5.50000715255737 15.625,6.62500762939453 14.375,6.62500762939453 14.375,5.50000715255737z M14.375,3.125L15.625,3.125 15.625,4.375 14.375,4.375 14.375,3.125z M11.4587602615356,3.125L12.9168796539307,3.125 12.9168796539307,4.375 11.4587602615356,4.375 11.4587602615356,3.125z M1.25,1.25L1.25,6.25 8.75,6.25 8.75,1.25 1.25,1.25z M0,0L10,0 10,3.125 10,4.375 10,7.5 5.625,7.5 5.625,8.62500762939453 5.625,8.75 6.875,8.75 6.875,10 3.13188362121582,10 3.13188362121582,8.75 4.375,8.75 4.375,8.62500762939453 4.375,7.5 0,7.5 0,0z";
        public const string STORAGE_TEST_PAGE_ICON = "M7.49997854232788,15.0000076293945L7.49997854232788,18.7499904632568 13.1249694824219,18.7499904632568 13.1249694824219,15.0000076293945 7.49997854232788,15.0000076293945z M9.99997520446777,4.37502670288086L9.99997520446777,6.87502288818359 13.1249694824219,6.87502288818359 13.1249694824219,4.37502670288086 9.99997520446777,4.37502670288086z M4.37498664855957,4.37501764297485L4.37498664855957,18.7499904632568 6.24998140335083,18.7499904632568 6.24998140335083,14.3750085830688 6.43310594558716,13.932900428772 6.87497997283936,13.7500095367432 13.7499675750732,13.7500095367432 14.1920757293701,13.932900428772 14.3749666213989,14.3750085830688 14.3749666213989,18.7499904632568 16.249963760376,18.7499904632568 16.249963760376,7.13372564315796 14.3749666213989,5.26805210113525 14.3749666213989,7.50002145767212 14.1920757293701,7.94189548492432 13.7499675750732,8.12502002716064 9.37497615814209,8.12502002716064 8.93310165405273,7.94189548492432 8.74997711181641,7.50002145767212 8.74997711181641,4.37501764297485 4.37498664855957,4.37501764297485z M3.74998784065247,3.12502002716064L13.7362174987793,3.12502002716064 13.7499675750732,3.12502884864807 14.2323913574219,3.35228490829468 14.2612524032593,3.39089846611023 17.3155994415283,6.43060159683228 17.4999618530273,6.87371635437012 17.4999618530273,19.3749904632568 17.3170738220215,19.8170948028564 16.874963760376,19.9999904632568 13.7501573562622,19.9999904632568 6.87497997283936,20 3.74998784065247,19.9999904632568 3.30788445472717,19.8170948028564 3.12498903274536,19.3749904632568 3.12498903274536,3.75001883506775 3.30788445472717,3.30788683891296 3.74998784065247,3.12502002716064z M0.624998867511749,0L9.99998188018799,0 9.99998188018799,1.24999761581421 1.2499977350235,1.24999761581421 1.2499977350235,15.6249704360962 0,15.6249704360962 0,0.624998867511749 0.182890295982361,0.182890295982361 0.624998867511749,0z";
    }
}

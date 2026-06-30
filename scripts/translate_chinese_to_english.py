#!/usr/bin/env python3
"""Replace Chinese text in source files with English equivalents."""

import re
import sys
from pathlib import Path

CJK_RE = re.compile(r"[\u4e00-\u9fff\u3400-\u4dbf\uf900-\ufaff]+")

# Longest phrases first for greedy replacement
PHRASES = [
    ("根据文件的创建日期对文件进行排序", "Sort files by creation date"),
    ("表示当前游戏所占用的物理内存大小", "Physical memory used by the current game"),
    ("表示当前游戏所使用的堆内存大小", "Heap memory used by the current game"),
    ("可以是图标名称也可以是物品名称", "May be an icon name or item name"),
    ("表示聊天命令是否被处理的任务", "Task indicating whether the chat command was handled"),
    ("批量删除指定玩家背包中的物品", "Batch delete items from a player's inventory"),
    ("和目标类型获取最新的传送记录", "Get the latest teleport record by target type"),
    ("是否启用游戏内货币兑换积分", "Whether in-game currency to points exchange is enabled"),
    ("是否在服务器启动时自动备份", "Whether to auto-backup on server startup"),
    ("这里可以加入更多的验证逻辑", "Additional validation logic can be added here"),
    ("获取指定项目的本地化字符串", "Get localized string for the specified item"),
    ("指当前游戏中的活跃对象数量", "Number of active objects in the current game"),
    ("支持请求范围和中间件注入", "Supports request scoping and middleware injection"),
    ("如果不存在则加载默认配置", "Load default configuration if it does not exist"),
    ("如果为空则返回或实体类名", "Return entity class name if empty"),
    ("表示堆内存的最大可用容量", "Maximum available heap memory capacity"),
    ("删除指定玩家背包中的物品", "Delete items from a player's inventory"),
    ("表示聊天消息钩子的静态类", "Static class representing chat message hooks"),
    ("是否在手动备份后重置计时", "Whether to reset timer after manual backup"),
    ("请求并将它们路由到正确的", "Route requests to the correct"),
    ("游戏内货币与积分兑换比例", "In-game currency to points exchange ratio"),
    ("是否在没有玩家时跳过备份", "Whether to skip backup when no players are online"),
    ("如果命令被其他功能处理", "If the command was handled by another feature"),
    ("和家园名称删除家园位置", "Delete home location by player and home name"),
    ("检查玩家周围是否有僵尸", "Check whether zombies are near the player"),
    ("和家园名称获取家园位置", "Get home location by player and home name"),
    ("获取指定消息的聊天钩子", "Get chat hook for the specified message"),
    ("判断两个玩家是否是好友", "Determine whether two players are friends"),
    ("为图标名称时后缀必须为", "Suffix must be set when using an icon name"),
    ("异步获取分页的积分信息", "Asynchronously get paginated points info"),
    ("确保不调用下一个中间件", "Ensure the next middleware is not invoked"),
    ("获取商品关联的物品清单", "Get item list associated with a product"),
    ("设置指定消息的聊天钩子", "Set chat hook for the specified message"),
    ("游戏内货币兑换积分命令", "In-game currency to points exchange command"),
    ("移除指定消息的聊天钩子", "Remove chat hook for the specified message"),
    ("可以设置从注释文件加载", "Can be configured to load from XML comment files"),
    ("列表异步获取积分字典", "Asynchronously get points dictionary from list"),
    ("获取指定目录中的所有", "Get all files in the specified directory"),
    ("获取所有玩家的领地石", "Get land claims for all players"),
    ("获取玩家背包物品数量", "Get item count in player inventory"),
    ("校验参数是否符合预期", "Validate parameters meet expectations"),
    ("获取指定玩家的领地石", "Get land claims for the specified player"),
    ("是否展示开发物品方块", "Whether to show developer item blocks"),
    ("从数据库或缓存中检索", "Retrieve from database or cache"),
    ("根据名称获取城市位置", "Get city location by name"),
    ("获取默认配置文件路径", "Get default configuration file path"),
    ("控制器及其依赖项由", "Controllers and dependencies are"),
    ("找到的所有文件路径", "All discovered file paths"),
    ("表示聊天钩子的缓存", "Cache for chat hooks"),
    ("获取指定玩家的背包", "Get inventory for the specified player"),
    ("新玩家进入多人游戏", "New player joined multiplayer"),
    ("指定消息的聊天钩子", "Chat hook for the specified message"),
    ("修改商品关联的命令", "Update commands associated with a product"),
    ("服务端正常运行时间", "Server uptime"),
    ("旧玩家加入多人游戏", "Returning player joined multiplayer"),
    ("数据库仓储相关类型", "Database repository related types"),
    ("下一个血月就在今天", "Next blood moon is today"),
    ("但是加载的内容可被", "but loaded content can be"),
    ("获取商品关联的命令", "Get commands associated with a product"),
    ("修改商品关联的物品", "Update items associated with a product"),
    ("检查请求路径是否为", "Check whether request path is"),
    ("重置的签到状态数量", "Number of reset sign-in states"),
    ("注册数据库仓储服务", "Register database repository services"),
    ("游戏中的区块数量", "Number of chunks in the game"),
    ("重定向到前端页面", "Redirect to frontend page"),
    ("保留文件数量限制", "Maximum number of files to retain"),
    ("获取备份文件列表", "Get backup file list"),
    ("堆内存的最大限制", "Maximum heap memory limit"),
    ("要处理的聊天钩子", "Chat hook to process"),
    ("分页获取历史玩家", "Get historical players with pagination"),
    ("控制器和操作方法", "Controllers and action methods"),
    ("获取指定历史玩家", "Get specified historical player"),
    ("服务器配置文件名", "Server configuration file name"),
    ("被击杀者实体信息", "Killed entity information"),
    ("积分信息仓储接口", "Points info repository interface"),
    ("获取所有在线玩家", "Get all online players"),
    ("直接跳过此次备份", "Skip this backup directly"),
    ("要移除的聊天钩子", "Chat hook to remove"),
    ("获取指定在线玩家", "Get specified online player"),
    ("处理 Steam 回调逻辑", "Handle Steam callback logic"),
    ("处理聊天命令", "Handle chat command"),
    ("处理聊天消息", "Handle chat message"),
    ("添加聊天钩子", "Add chat hook"),
    ("移除聊天钩子", "Remove chat hook"),
    ("聊天消息", "Chat message"),
    ("聊天命令", "Chat command"),
    ("聊天钩子", "Chat hook"),
    ("在线玩家", "Online player"),
    ("配置管理器", "Configuration manager"),
    ("配置改变事件", "Configuration changed event"),
    ("全局配置", "Global settings"),
    ("获取配置", "Get configuration"),
    ("配置类型", "Configuration type"),
    ("城市位置", "City location"),
    ("传送需要积分", "Points required for teleport"),
    ("三维坐标", "3D coordinates"),
    ("视角方向", "View direction"),
    ("传送记录", "Teleport record"),
    ("签到奖励积分", "Sign-in reward points"),
    ("玩家总积分", "Player total points"),
    ("游戏币数量", "In-game currency amount"),
    ("变量基类", "Variables base class"),
    ("玩家Id", "Player Id"),
    ("玩家名称", "Player name"),
    ("实体Id", "Entity Id"),
    ("传送间隔", "Teleport interval"),
    ("需要积分", "Points required"),
    ("目标名称", "Target name"),
    ("冷却时间", "Cooldown time"),
    ("城市Id", "City Id"),
    ("城市名称", "City name"),
    ("Home名称", "Home name"),
    ("拥有积分", "Points balance"),
    ("上次签到天数", "Last sign-in day count"),
    ("上次签到日期", "Last sign-in date"),
    ("玩家跨平台Id", "Player cross-platform Id"),
    ("积分信息", "Points info"),
    ("创建索引", "Create index"),
    ("迁移数据到新表", "Migrate data to new table"),
    ("首次 或 跨天", "First run or crossed day boundary"),
    ("用户名", "Username"),
    ("密码", "Password"),
    ("服务器地址", "Server address"),
    ("数据库路径", "Database path"),
    ("配置", "Settings"),
    ("商品", "Goods"),
    ("价格", "Price"),
    ("名称", "Name"),
    ("创建日期", "Created date"),
    ("唯一Id", "Unique Id"),
    ("统计", "Statistics"),
    ("动物数", "Animal count"),
    ("僵尸数", "Zombie count"),
    ("玩家数", "Player count"),
    ("欢迎", "Welcome"),
    ("命令执行回复", "Command execution reply"),
    ("单位", "Unit"),
    ("秒", "seconds"),
]

WORDS = [
    ("跨平台", "cross-platform"),
    ("本地化", "localization"),
    ("仓储", "repository"),
    ("异步", "asynchronous"),
    ("分页", "pagination"),
    ("备份", "backup"),
    ("传送", "teleport"),
    ("积分", "points"),
    ("玩家", "player"),
    ("服务器", "server"),
    ("配置", "configuration"),
    ("商品", "goods"),
    ("城市", "city"),
    ("家园", "home"),
    ("实体", "entity"),
    ("名称", "name"),
    ("创建", "created"),
    ("日期", "date"),
    ("唯一", "unique"),
    ("冷却", "cooldown"),
    ("时间", "time"),
    ("目标", "target"),
    ("间隔", "interval"),
    ("需要", "required"),
    ("获取", "Get"),
    ("设置", "Set"),
    ("删除", "Delete"),
    ("修改", "Update"),
    ("添加", "Add"),
    ("移除", "Remove"),
    ("处理", "Handle"),
    ("检查", "Check"),
    ("表示", "Represents"),
    ("是否", "Whether"),
    ("数量", "count"),
    ("列表", "list"),
    ("命令", "command"),
    ("消息", "message"),
    ("钩子", "hook"),
    ("在线", "online"),
    ("历史", "historical"),
    ("指定", "specified"),
    ("所有", "all"),
    ("默认", "default"),
    ("全局", "global"),
    ("管理器", "manager"),
    ("事件", "event"),
    ("类型", "type"),
    ("信息", "info"),
    ("位置", "location"),
    ("方向", "direction"),
    ("坐标", "coordinates"),
    ("视角", "view"),
    ("价格", "price"),
    ("拥有", "owned"),
    ("上次", "last"),
    ("签到", "sign-in"),
    ("奖励", "reward"),
    ("游戏币", "in-game currency"),
    ("变量", "variables"),
    ("基类", "base class"),
    ("统计", "statistics"),
    ("动物", "animal"),
    ("僵尸", "zombie"),
    ("欢迎", "welcome"),
    ("回复", "reply"),
    ("执行", "execution"),
    ("单位", "unit"),
    ("索引", "index"),
    ("迁移", "migrate"),
    ("数据", "data"),
    ("新表", "new table"),
    ("首次", "first"),
    ("跨天", "crossed day"),
    ("用户名", "username"),
    ("密码", "password"),
    ("地址", "address"),
    ("路径", "path"),
    ("文件", "file"),
    ("目录", "directory"),
    ("接口", "interface"),
    ("实现", "implementation"),
    ("方法", "method"),
    ("参数", "parameter"),
    ("返回", "return"),
    ("任务", "task"),
    ("缓存", "cache"),
    ("注册", "register"),
    ("服务", "service"),
    ("控制器", "controller"),
    ("操作", "action"),
    ("中间件", "middleware"),
    ("验证", "validation"),
    ("逻辑", "logic"),
    ("回调", "callback"),
    ("重定向", "redirect"),
    ("前端", "frontend"),
    ("页面", "page"),
    ("区块", "chunk"),
    ("背包", "inventory"),
    ("物品", "item"),
    ("图标", "icon"),
    ("领地石", "land claim"),
    ("好友", "friend"),
    ("血月", "blood moon"),
    ("今天", "today"),
    ("启动", "startup"),
    ("手动", "manual"),
    ("计时", "timer"),
    ("重置", "reset"),
    ("跳过", "skip"),
    ("比例", "ratio"),
    ("兑换", "exchange"),
    ("启用", "enabled"),
    ("展示", "show"),
    ("开发", "developer"),
    ("方块", "block"),
    ("内存", "memory"),
    ("堆", "heap"),
    ("物理", "physical"),
    ("最大", "maximum"),
    ("限制", "limit"),
    ("活跃", "active"),
    ("对象", "object"),
    ("游戏", "game"),
    ("当前", "current"),
    ("运行", "running"),
    ("正常", "normal"),
    ("时间", "time"),
    ("击杀", "kill"),
    ("被", "by"),
    ("者", ""),
    ("的", " "),
    ("和", "and"),
    ("或", "or"),
    ("为", "as"),
    ("在", "in"),
    ("到", "to"),
    ("从", "from"),
    ("将", ""),
    ("对", "for"),
    ("用", "use"),
    ("由", "by"),
    ("可", "can"),
    ("被", ""),
    ("不", "not"),
    ("无", "no"),
    ("有", "has"),
    ("时", "when"),
    ("后", "after"),
    ("前", "before"),
    ("内", "in"),
    ("外", "outside"),
    ("中", "in"),
    ("上", "on"),
    ("下", "below"),
    ("等", "etc"),
    ("类", "class"),
    ("静态", "static"),
    ("异步", "async"),
    ("批量", "batch"),
    ("最新", "latest"),
    ("记录", "record"),
    ("关联", "associated"),
    ("清单", "list"),
    ("字典", "dictionary"),
    ("范围", "scope"),
    ("注入", "injection"),
    ("加载", "load"),
    ("内容", "content"),
    ("注释", "comment"),
    ("发现", "discovered"),
    ("找到", "found"),
    ("确保", "ensure"),
    ("调用", "invoke"),
    ("下一个", "next"),
    ("加入", "joined"),
    ("进入", "entered"),
    ("多人", "multiplayer"),
    ("状态", "state"),
    ("保留", "retain"),
    ("文件数量", "file count"),
    ("周围", "nearby"),
    ("后缀", "suffix"),
    ("必须", "must"),
    ("为空", "empty"),
    ("否则", "otherwise"),
    ("存在", "exists"),
    ("不存在", "does not exist"),
    ("如果", "if"),
    ("但是", "but"),
    ("直接", "directly"),
    ("此次", "this"),
    ("根据", "by"),
    ("排序", "sort"),
    ("支持", "supports"),
    ("请求", "request"),
    ("路由", "route"),
    ("正确", "correct"),
    ("功能", "feature"),
    ("其他", "other"),
    ("功能处理", "feature handling"),
    ("项目", "item"),
    ("字符串", "string"),
    ("占用", "used"),
    ("使用", "used"),
    ("所用", "used"),
    ("容量", "capacity"),
    ("可用", "available"),
    ("活跃对象", "active objects"),
    ("正常运行", "uptime"),
    ("运行时间", "uptime"),
    ("区块数量", "chunk count"),
    ("动物数", "animal count"),
    ("僵尸数", "zombie count"),
    ("玩家数", "player count"),
]


def translate_segment(text: str) -> str:
    original = text
    for zh, en in PHRASES:
        text = text.replace(zh, en)
    if not CJK_RE.search(text):
        return cleanup(text)

    for zh, en in WORDS:
        text = text.replace(zh, en)

    if CJK_RE.search(text):
        text = CJK_RE.sub("", text)

    return cleanup(text)


def cleanup(text: str) -> str:
    text = re.sub(r"\s+", " ", text)
    text = re.sub(r"\s+([,.;:])", r"\1", text)
    text = re.sub(r"\(\s+", "(", text)
    text = re.sub(r"\s+\)", ")", text)
    text = text.strip(" ,.;:")
    if text and text[0].islower():
        text = text[0].upper() + text[1:]
    return text


def translate_line(line: str) -> str:
    if not CJK_RE.search(line):
        return line

    def repl(match: re.Match[str]) -> str:
        return translate_segment(match.group(0))

    return CJK_RE.sub(repl, line)


def process_file(path: Path) -> bool:
    original = path.read_text(encoding="utf-8")
    lines = original.splitlines(keepends=True)
    changed = False
    new_lines = []
    for line in lines:
        new_line = translate_line(line)
        if new_line != line:
            changed = True
        new_lines.append(new_line)
    if changed:
        path.write_text("".join(new_lines), encoding="utf-8")
    return changed


def main() -> int:
    root = Path(__file__).resolve().parents[1] / "src" / "SdtdMultiServerKit"
    changed_files = 0
    for pattern in ("**/*.cs", "**/*.sql"):
        for path in root.glob(pattern):
            if process_file(path):
                changed_files += 1
                print(f"updated: {path.relative_to(root.parents[1])}")

    remaining = 0
    for pattern in ("**/*.cs", "**/*.sql"):
        for path in root.glob(pattern):
            text = path.read_text(encoding="utf-8")
            remaining += len(CJK_RE.findall(text))
    print(f"Changed files: {changed_files}")
    print(f"Remaining CJK segments: {remaining}")
    return 0 if remaining == 0 else 1


if __name__ == "__main__":
    sys.exit(main())
